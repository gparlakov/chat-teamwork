var ui = (function () {

    function buildLoginForm() {
        var html =
            '<div id="login-form-holder">' +
				'<form>' +
                	'<a href="#" id="btn-show-login" class="button selected">Login</a>' +
					'<a href="#" id="btn-show-register" class="button">Register</a>' +
					'<div id="login-form">' +
						    '<label for="tb-login-username">Username: </label>' +
						    '<input type="text" id="tb-login-username"><br />' +
						    '<label for="tb-login-password">Password: </label>' +
						    '<input type="text" id="tb-login-password"><br />' +
						    '<button id="btn-login" class="button">Login</button>' +
					'</div>' +
					'<div id="register-form" style="display: none">' +
						'<label for="tb-register-username">Username: </label>' +
						'<input type="text" id="tb-register-username"><br />' +
						'<label for="tb-register-nickname">Nickname: </label>' +
						'<input type="text" id="tb-register-nickname"><br />' +
						'<label for="tb-register-password">Password: </label>' +
						'<input type="text" id="tb-register-password"><br />' +
						'<button id="btn-register" class="button">Register</button>' +
					'</div>' +
				'</form>' +
				'<div id="error-messages"></div>' +
            '</div>';
        return html;
    }

    function buildChatUI(user) {
        var html = '<div id="header" class="clearfix">' +
                '<div id="avatar" style="background-image: url(' + user.imageUrl + ');background-size:72px">' +
                '<input id="change-avatar" type="file" class="hidden-input-avatar-change" /></div>' +
                '<span id="user-nickname">' + user.nick +
		        '</span>' +
                '<button id="btn-logout">Logout</button><br/>' +
            '</div>' +
            '<div id="chat-content" class="clearfix">' +
                '<div id="notification"><ul id="notification-list"></ul>' +
                '</div>' +
                '<div id="msg-content">' +
                '</div>' +
                '<div id="user-list">' +                    
                '</div>' +
            '</div>' +
            '<div id="input">' +
                '<input type="text" id="msg-text"/>' +
                '<button id="message-send-button-new">Send</button>' +
                '<button id="file-send-button-holder"><div>Send File<input id="file-send-button" type="file" class="hidden-input-file" /></div></button>' +
                //'<button id="msg-history">History</button>' +
            '</div>';
        return html;
    }

    function buildUserList(users) {
        ///
        var selected = $(".userSelected").data("user-id");
        ///

        var list = '<ul id="users-sidebar" class="game-list open-games">';
        for (var i = 0; i < users.length; i++) {
            var user = users[i];
            if (user.id === selected) {
                list +=
                '<li data-user-id="' + user.id + '"' + ' class="userSelected">' +
                    //'<a href="#" >' +
                    //	$("<div />").html(game.title).text() +
                    //'</a>' +
                    //'<span> by ' +
                        user.nickname +
                    //'</span>' +
                '</li>';
            }
            else {
                list +=
                    '<li data-user-id="' + user.id + '">' +
                        //'<a href="#" >' +
                        //	$("<div />").html(game.title).text() +
                        //'</a>' +
                        //'<span> by ' +
                            user.nickname +
                        //'</span>' +
                    '</li>';
            }
        }
        list += "</ul>";
        return list;
    }

    function loadMessages(messages, fromUserId) {
        if (messages.length === 0) {
            $("#msg-content").html("");
        }

        var messagesList = "<ul id='messages-list' data-from-user-id=" + fromUserId + ">";

        for (var i = 0; i < messages.length; i++) {
            var message = messages[i];
            var content = message.Content;

            if (content.indexOf("https://dl.dropboxusercontent.com") >= 0) {
                content = "<a href ='" + content +"' >File - click to open</a>"
            }

            messagesList += "<li class='message-from-other'>" + message.FromUserNickname + ": " + content + "</li>";
        }

        messagesList += "</ul>";

        $("#msg-content").html(messagesList);
    };

    //    var list = '<ul class="game-list active-games">';
    //    for (var i = 0; i < gamesList.length; i++) {
    //        var game = gamesList[i];
    //        list +=
	//			'<li class="game-status-' + game.status + '" data-game-id="' + game.id + '" data-creator="' + game.creatorNickname + '">' +
	//				'<a href="#" class="btn-active-game">' +
	//					$("<div />").html(game.title).text() +
	//				'</a>' +
	//				'<span> by ' +
	//					game.creatorNickname +
	//				'</span>' +
	//			'</li>';
    //    }
    //    list += "</ul>";
    //    return list;
    //}

    //function buildGuessTable(guesses) {
    //    var tableHtml =
	//		'<table border="1" cellspacing="0" cellpadding="5">' +
	//			'<tr>' +
	//				'<th>Number</th>' +
	//				'<th>Cows</th>' +
	//				'<th>Bulls</th>' +
	//			'</tr>';
    //    for (var i = 0; i < guesses.length; i++) {
    //        var guess = guesses[i];
    //        tableHtml +=
	//			'<tr>' +
	//				'<td>' +
	//					guess.number +
	//				'</td>' +
	//				'<td>' +
	//					guess.cows +
	//				'</td>' +
	//				'<td>' +
	//					guess.bulls +
	//				'</td>' +
	//			'</tr>';
    //    }
    //    tableHtml += '</table>';
    //    return tableHtml;
    //}

    //function buildGameState(gameState) {
    //    var html =
	//		'<div id="game-state" data-game-id="' + gameState.id + '">' +
	//			'<h2>' + gameState.title + '</h2>' +
	//			'<div id="blue-guesses" class="guess-holder">' +
	//				'<h3>' +
	//					gameState.blue + '\'s gueesses' +
	//				'</h3>' +
	//				buildGuessTable(gameState.blueGuesses) +
	//			'</div>' +
	//			'<div id="red-guesses" class="guess-holder">' +
	//				'<h3>' +
	//					gameState.red + '\'s gueesses' +
	//				'</h3>' +
	//				buildGuessTable(gameState.redGuesses) +
	//			'</div>' +
	//	'</div>';
    //    return html;
    //}

    return {
        chatUI: buildChatUI,
        userList: buildUserList,
        loginForm: buildLoginForm,
        loadMessages: loadMessages,
        //gameState: buildGameState,
        //messagesList: buildMessagesList
    }

}());