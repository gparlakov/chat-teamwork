/// <reference path="http-requester.js" />
/// <reference path="class.js" />
/// <reference path="http://crypto-js.googlecode.com/svn/tags/3.1.2/build/rollups/sha1.js" />
var persisters = (function () {
    var nickname = localStorage.getItem("nickname");
    var sessionKey = localStorage.getItem("sessionKey");
    var imageUrl = localStorage.getItem("imageUrl");

    function saveUserData(userData) {
        localStorage.setItem("nickname", userData.nickname);
        localStorage.setItem("sessionKey", userData.sessionKey);
        localStorage.setItem("imageUrl", userData.imageUrl)
        nickname = userData.nickname;
        sessionKey = userData.sessionKey;
        imageUrl = userData.imageUrl;
    }

    function clearUserData() {
        localStorage.removeItem("nickname");
        localStorage.removeItem("sessionKey");
        localStorage.removeItem("imageUrl");
        nickname = "";
        sessionKey = "";
        imageUrl = "";
    }

    var MainPersister = Class.create({
        init: function (rootUrl) {
            this.rootUrl = rootUrl;
            this.user = new UserPersister(this.rootUrl);
            this.message = new MessagesPersister(this.rootUrl);
        },
        isUserLoggedIn: function () {
            var isLoggedIn = nickname != null && sessionKey != null;
            return isLoggedIn;
        },
        nickname: function () {
            var user = {
                nick: nickname,
                imageUrl: imageUrl
            }
            return user;
        }
    });
    var UserPersister = Class.create({
        init: function (rootUrl) {
            //...api/user/
            this.rootUrl = rootUrl + "users/";
        },
        login: function (user, success, error) {
            var url = this.rootUrl + "login";
            var userData = {
                username: user.username,
                password: CryptoJS.SHA1(user.username + user.password).toString()
            };

            httpRequester.postJSON(url, userData,
				function (data) {
				    saveUserData(data);
				    success(data);
				}, error);
        },
        register: function (user, success, error) {
            var url = this.rootUrl + "register";
            var userData = {
                username: user.username,
                nickname: user.nickname,
                password: CryptoJS.SHA1(user.username + user.password).toString()
            };
            httpRequester.postJSON(url, userData,
				function (data) {
				    saveUserData(data);
				    success(data);
				}, error);
        },
        logout: function (success, error) {
            var url = this.rootUrl + "logout/" + sessionKey;
            httpRequester.getJSON(url, function (data) {
                clearUserData();
                success(data);
            }, error)
        },
        getAll: function (success, error) {
            var url = this.rootUrl + sessionKey;
            httpRequester.getJSON(url, success, error)
        }
    });
    var MessagesPersister = Class.create({
        init: function (url) {
            this.rootUrl = url + "messages/";
        },
        unread: function (success, error) {
            var url = this.rootUrl + "unread/" + sessionKey;
            httpRequester.getJSON(url, success, error);
        },
        all: function (userId,success, error) {
            var url = this.rootUrl + sessionKey +"/idOfSender/" + userId;
            httpRequester.getJSON(url, success, error);
        },
        send: function (data, success, error) {
            var url = this.rootUrl;
            data.fromUser = sessionKey;
            httpRequester.postJSON(url, data, success, error);
        },
        fileUpload: function () {

        }
    });
    return {
        get: function (url) {
            return new MainPersister(url);
        }
    };
}());