var socket = io();

$('document').ready(function()
{
	socket.emit('connect', "");
});

socket.on('update', function(msg)
{
    console.log(msg);
});