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
            if (error) return generateErrorPage();

            var ordre = parameters.split("&")[1].split("=")[1];

            var ordreCfg = cfg.readOrdreConfig();

            var currentOrder = ordreCfg[ordre];

            var menu = [];

            var cont = false;

            if(currentOrder != null)
            {
                connection.query("select * from food_types;", function (error2, results2, fields2)
                {

                    connection.query("select * from food;", function (error3, results3, fields3)
                    {

                        for(cO in currentOrder.types)
                        {

                            for(var i = 0, length1 = results2.length; i < length1; i++)
                            {

                                if(cO == results2[i].id)
                                {

                                    var foodType = [results2[i].id, results2[i].name];

                                    var food = [];

                                    for(var i2 = 0, length3 = results3.length; i2 < length3; i2++)
                                    {

                                        if(results3[i2].type_id == results2[i].id)
                                        {
                                            food.push([results3[i2].id, results3[i2].name]);
                                        }
                                    }

                                    foodType.push(food);

                                    menu.push(foodType);

                                }
                            }

                        }

                        if(menu != [])
                        {
                            var page = '';

                            for(m of menu)
                            {
                                page += ('<div class="foodType" id="fT-'+m[0]+'" ><div class="foodsContainer" style="visibility: hidden;">');

                                    for(f of m[2])
                                    {
                                        page += ('<div class="food" id="f-'+f[0]+'">'+f[1]+'</div>');
                                    }

                                page += '</div></div>';
                            }
                                 
                            callback(page);
                        }
                        else
                        {
                            return generateErrorPage();
                        }
                    });
                });            
            }
            else
            {
                return generateErrorPage();
            }
        });
    }

    connect();

}());