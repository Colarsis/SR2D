(function() {
    var fs = require("fs");
    var cfg = require("./configFile");
    var mysql = require('mysql');

    var connection;

    function connect()
    {
        var coCfg = cfg.readDatabaseConfig();
        var nu = [null, null, null, null, null];

        if(coCfg != nu)
        {

            connection = mysql.createConnection({
              host     : coCfg[4],
              user     : coCfg[1],
              password : coCfg[2],
              database : coCfg[0],
              port     : coCfg[3]
          });

            connection.connect();
        }
        else
        {
            throw new Error('E03');
        }
    }

    module.exports.generatePage = function(parameters, callback) 
    {
        connection.query("select * from badges where code_id='"+parameters.split("&")[0].split("=")[1]+"';", function (error, results, fields) 
        {
            if (error) return error;

            var page =  '<!DOCTYPE html>'+
                        '<html>'+
                            '<head>'+
                                '<meta charset="utf8"/>'+
                                '<title>SR2D</title>'+
                                '<link rel="stylesheet" href="css/menu.css">'+
                            '</head>'+

                            '<script src="js/socket.io.js"></script>'+
                            '<script src="http://code.jquery.com/jquery-1.11.1.js"></script>'+
                            '<script src="js/socket.js"></script>'+
                            '<body>'+
                                '<article id="test">'+
                                        '<span style="position: absolute; top: 100px; left: 360px;">'+
                                        '<p>Bonjour '+results[0].first_name+' '+results[0].name+'</p>'+
                                        '</span>'+
                                '</article>'+ 
                            '</body>'+
                        '</html>';

                        callback(page);
        });
    }

    connect();

}());