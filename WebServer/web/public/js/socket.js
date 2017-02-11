var socket = io();

$('document').ready()
{
	socket.emit('connect', "");
}

socket.on('update', function(msg){
    console.log(msg)});