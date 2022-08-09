"use strict";

var client = new signalR.HubConnectionBuilder().withUrl("/callHub").build();
client.start();
client.on("ReceivedNumber", function (room, count) {
    $('#Room' + room).children().find('h1[name="roomNO"]').html(`${count}`);
    $('#' + room).parents('tr').find('div[name="roomNO"]').html(`${count}`);
});
