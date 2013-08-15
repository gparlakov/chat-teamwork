/// <reference path="class.js" />
/// <reference path="persister.js" />
/// <reference path="jquery-2.0.2.js" />
/// <reference path="ui.js" />

var controllers = (function () {

    var updateTimer = 10000;

    var local = "http://localhost:17097/api/";
    var remote = "http://chat-teamwork.apphb.com/api/";

    var rootUrl = "http://chat-teamwork.apphb.com/api/";
    
    var Controller = Class.create({
        persister:{},
        init: function () {
            this.persister = persisters.get(rootUrl);            
        },
        initNotifier: function () {
            var notifier = PUBNUB.init({
                subscribe_key: 'sub-c-484fa9d0-057c-11e3-991c-02ee2ddab7fe',
                publish_key: 'pub-c-01a2bb9b-25f7-4385-8cd3-7c10036a6b06'
            });
            var notifierWrapper = $("#notification-list");
            
            notifier.subscribe({
                channel: "chat-michelangelo-channel",
                message: function (m) {
                    var message = JSON.parse(m);
                    if (message.toUser == localStorage["nickname"]) {
                        var notification = "<li class='notification'>New message from " + message.fromUser + "</li>";

                        var notificationsAll = notifierWrapper.html();
                        notificationsAll = notification + notificationsAll;
                        notifierWrapper.html(notificationsAll);
                    };                   
                },
                //connect: publish
            })
        },
        loadUI: function (selector) {
            if (this.persister.isUserLoggedIn()) {
                this.loadChatUI(selector);
            }
            else {
                this.loadLoginFormUI(selector);
            }
            this.attachUIEventHandlers(selector);            
        },
        loadLoginFormUI: function (selector) {
            var loginFormHtml = ui.loginForm()
            $(selector).html(loginFormHtml);
        },
        loadChatUI: function (selector) {
            var self = this;
            var chatUIHtml =
				ui.chatUI(this.persister.nickname());
            $(selector).html(chatUIHtml);

            this.updateUI(selector);

            setInterval(function () {
                self.updateUI(selector);
            }, updateTimer);

            this.initNotifier();
        },
        loadUsers: function () {
            this.persister.user.getAll(function (users) {
                var usersListHtml = ui.userList(users);
                $("#user-list").html(usersListHtml);
                //var gameHtml = ui.gameState(gameState);
                //$(selector + " #game-holder").html(gameHtml)
            }, function (err) {
                var text = err.statusText;
                alert(text);
            });
        },
        attachUIEventHandlers: function (selector) {
            var wrapper = $(selector);
            var self = this;

            wrapper.on("click", "#btn-show-login", function () {
                wrapper.find(".button.selected").removeClass("selected");
                $(this).addClass("selected");
                wrapper.find("#login-form").show();
                wrapper.find("#register-form").hide();
            });
            wrapper.on("click", "#btn-show-register", function () {
                wrapper.find(".button.selected").removeClass("selected");
                $(this).addClass("selected");
                wrapper.find("#register-form").show();
                wrapper.find("#login-form").hide();
            });
            wrapper.on("click", "#btn-login", function () {
                var user = {
                    username: $(selector + " #tb-login-username").val(),
                    password: $(selector + " #tb-login-password").val()
                }

                self.persister.user.login(user, function () {
                    self.loadChatUI(selector);
                    self.loadUsers();
                }, function (err) {
                    var text = err.statusText;
                    wrapper.find("#error-messages").text(text);
                });
                return false;
            });

            wrapper.on("click", "#btn-register", function () {
                var user = {
                    username: $(selector).find("#tb-register-username").val(),
                    nickname: $(selector).find("#tb-register-nickname").val(),
                    password: $(selector).find("#tb-register-password").val()
                }
                self.persister.user.register(user, function () {
                    self.loadChatUI(selector);
                }, function (err) {
                    wrapper.find("#error-messages").text("Bla");
                });
                return false;
            });

            wrapper.on("click", "#btn-logout", function () {
                self.persister.user.logout(function () {
                    self.loadLoginFormUI(selector);
                    clearInterval(updateTimer);
                }, function (err) {
                });
            });

            wrapper.on("click", "#users-sidebar > li", function (e) {
                var userId = $(this).data("user-id");
                
                self.persister.message.all(userId ,function (messages) {
                    ui.loadMessages(messages, userId);
                }, function (err) { alert(err.responseText); })
                
            });

            wrapper.on("click", "#message-send-button-new", function (e) {
                var data = {};

                var messagesList = $("#messages-list");
                data.toUser = messagesList.data("from-user-id");

                var inputText = $("#msg-text")
                data.content = inputText.val();

                self.persister.message.send(data, function () {
                    inputText.val("").attr("placeholder", "Message sent!");
                    $("#messages")
                }, function () {
                    inputText.val("").attr("placeholder", "Message NOT sent!");
                });

                //var userId = $(this).data("user-id");

                //self.persister.message.all(function (messages) {
                    


                //}, function (err) { alert(err.responseText); })

            });

            $("#file-send-button").on("click", function (ev) {                                 
                var messagesList = $("#messages-list");
                if (messagesList.length === 0 ) {
                    alert("No user to send to!");
                    ev.preventDefault();
                    return;
                }

                var sessionKey = localStorage["sessionKey"];
                var id = messagesList.data("from-user-id");
                var urlToSendFileTo = rootUrl + "messages/" + sessionKey + "/" + id;

                $(this).html5Uploader({
                    postUrl: urlToSendFileTo,
                })
                
            });

            //    html5Uploader({
            //    postUrl: getUrl(),
            //});
            
        },
        updateUI: function (selector) {
            var self = this;
            this.loadUsers();
            this.loadChatMessages();
        },
        loadChatMessages: function(){
            var messagesList = $("#messages-list");
            if (messagesList.length > 0) {
                var userChatWithId = messagesList.data("from-user-id");

                this.persister.message.all(userChatWithId, function (messages) {
                    ui.loadMessages(messages, userChatWithId);
                }, function (err) { alert(err.responseText); })

            }
        }
    });
    return {
        get: function () {
            return new Controller();
        }
    }
}());

$(function () {
    var controller = controllers.get();
    controller.loadUI("#content");
});