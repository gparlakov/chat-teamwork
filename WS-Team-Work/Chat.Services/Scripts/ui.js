﻿var ui = (function () {

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
                '<div id="avatar" style="background-image: url(' + user.avatar + ')">' +
                '</div>' +
                '<span id="user-nickname">' + user.nick +
		        '</span>' +
                '<button id="btn-logout">Logout</button><br/>' +
            '</div>' +
            '<div id="chat-content" class="clearfix">' +
                '<div id="notification">' +
                '</div>' +
                '<div id="msg-content">' +
                '</div>' +
                '<div id="user-list">' +
                    //'<ul>'+
                    //    '<li>'+
                    //        'Mimi'+
                    //    '</li>'+
                    //    '<li>'+
                    //        'Pesho'+
                    //    '</li>'+
                    //'</ul>'+
                '</div>' +
            '</div>' +
            '<div id="input">' +
                '<input type="text" id="msg-text"/>' +
                '<button id="msg-send">Send</button>' +
                '<button id="msg-file">Browse</button>' +
                '<button id="msg-history">History</button>' +
            '</div>';
        return html;
    }

    function buildUserList(users) {
        var list = '<ul id="users-sidebar" class="game-list open-games">';
        for (var i = 0; i < users.length; i++) {
            var user = users[i];
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
        list += "</ul>";
        return list;
    }

    function buildActiveGamesList(games) {
        var gamesList = Array.prototype.slice.call(games, 0);
        gamesList.sort(function (g1, g2) {
            if (g1.status == g2.status) {
                return g1.title > g2.title;
            }
            else {
                if (g1.status == "in-progress") {
                    return -1;
                }
            }
            return 1;
        });

        var list = '<ul class="game-list active-games">';
        for (var i = 0; i < gamesList.length; i++) {
            var game = gamesList[i];
            list +=
				'<li class="game-status-' + game.status + '" data-game-id="' + game.id + '" data-creator="' + game.creatorNickname + '">' +
					'<a href="#" class="btn-active-game">' +
						$("<div />").html(game.title).text() +
					'</a>' +
					'<span> by ' +
						game.creatorNickname +
					'</span>' +
				'</li>';
        }
        list += "</ul>";
        return list;
    }

    function buildGuessTable(guesses) {
        var tableHtml =
			'<table border="1" cellspacing="0" cellpadding="5">' +
				'<tr>' +
					'<th>Number</th>' +
					'<th>Cows</th>' +
					'<th>Bulls</th>' +
				'</tr>';
        for (var i = 0; i < guesses.length; i++) {
            var guess = guesses[i];
            tableHtml +=
				'<tr>' +
					'<td>' +
						guess.number +
					'</td>' +
					'<td>' +
						guess.cows +
					'</td>' +
					'<td>' +
						guess.bulls +
					'</td>' +
				'</tr>';
        }
        tableHtml += '</table>';
        return tableHtml;
    }

    function buildGameState(gameState) {
        var html =
			'<div id="game-state" data-game-id="' + gameState.id + '">' +
				'<h2>' + gameState.title + '</h2>' +
				'<div id="blue-guesses" class="guess-holder">' +
					'<h3>' +
						gameState.blue + '\'s gueesses' +
					'</h3>' +
					buildGuessTable(gameState.blueGuesses) +
				'</div>' +
				'<div id="red-guesses" class="guess-holder">' +
					'<h3>' +
						gameState.red + '\'s gueesses' +
					'</h3>' +
					buildGuessTable(gameState.redGuesses) +
				'</div>' +
		'</div>';
        return html;
    }

    return {
        chatUI: buildChatUI,
        userList: buildUserList,
        loginForm: buildLoginForm,
        activeGamesList: buildActiveGamesList,
        gameState: buildGameState,
        //messagesList: buildMessagesList
    }

}());