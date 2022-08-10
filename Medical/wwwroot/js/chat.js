
"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();

//Disable send button until connection is established
document.getElementById("sendButton").disabled = true;

connection.on("ReceiveMessage", function (sendUser, message) {
    let user = $("#inpUser").val();
    let userName = $("#inpUser2").val();
    var MSN = document.querySelector("#MSN");
    var time = moment(Date.now()).format("LTS")
    var role = "訪客";
    var role1 = "客服";
    var userName1 = "訪客";
    var userName2 = "客服";
    if (userName == "") {
        userName1 = "訪客";
    }
    if (user == 1) {
        userName1 = userName;
        userName2 = "客服";
    }
    if (user == 2 || user == 3) {
        role = "客服";
        role1 = "訪客";
        userName1 = "客服";
        if (userName == "") {
            userName2 = "訪客";
        }
        //這邊是寫死的，之後要優化可以優化
        else {
            userName2 = "陳小名";
        }
    }
   
    if (user == sendUser) {
        
        $("#MSN").append(`<div class="direct-chat-msg text-md-end">
                                <div class="direct-chat-infos clearfix">
                                    <span class="direct-chat-name float-left" id="name_player1">《${userName1}》</span>
                                    <span class="direct-chat-timestamp float-right" id="time_player1">(${time})</span>
                                </div>
                                <span class="text-bold" style="font-size:25px;background-color:#ACD6FF;border-radius:10px;border:2px solid #F0F0F0">   ${message}  </span>
                                <img class="direct-chat-img" src="../img/${role}.jpg" alt="Message User Image" style="border-radius:50%">
                            <div class="direct-chat-text" >
                            </div>
                            </div>`)

    }
    else{
        $("#MSN").append(`<div class="direct-chat-msg">
                        <div class="direct-chat-infos clearfix">
                            <span class="direct-chat-name float-left" id="name_player1">《${userName2}》</span>
                            <span class="direct-chat-timestamp float-right" id="time_player1">(${time})</span>
                        </div>
                        <img class="direct-chat-img" src="../img/${role1}.jpg" alt="Message User Image" style="border-radius:50%">
                        <span class="text-bold" style="font-size:25px;background-color:#FFF4C1	;border-radius:10px;border:2px solid #F0F0F0">   ${message}  </span>
                    <div class="direct-chat-text">
                        
                    </div>
                    </div>`)
    }
});


connection.start().then(function () {
    document.getElementById("sendButton").disabled = false;
}).catch(function (err) {
    return console.error(err.toString());
});

document.getElementById("sendButton").addEventListener("click", function (event) {
    var sendUser = document.getElementById("inpUser").value;
    var message = document.getElementById("messageInput").value;
    connection.invoke("SendMessage",sendUser,message).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});

