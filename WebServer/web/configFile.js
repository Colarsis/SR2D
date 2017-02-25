(function() {

    var fs = require("fs");

    module.exports.readDatabaseConfig = function() {

        var cfgFile = fs.readFileSync("../web/cfg/connection.json");

        var jsonCfgFile = JSON.parse(cfgFile);

        var result = [null, null, null, null, null];

        for(co in jsonCfgFile["Connections"])
        {
        	if(jsonCfgFile["Connections"][co]["DefaultCo"] == "Yes")
            {
                result[0] = jsonCfgFile["Connections"][co]["DatabaseName"]["DatabaseName"];
                result[1] = jsonCfgFile["Connections"][co]["Username"]["Username"];
                result[2] = jsonCfgFile["Connections"][co]["Password"]["Password"];
                result[3] = jsonCfgFile["Connections"][co]["Port"]["Port"];
                result[4] = jsonCfgFile["Connections"][co]["Source"]["Source"];

                return result;
            }
        }

        console.log("No default database connection found");

        return result;

    }

    module.exports.readSynchroConfig = function() {

        var cfgFile = fs.readFileSync("../web/cfg/synchro.json")

        var jsonCfgFile = JSON.parse(cfgFile);

        var result = [null, null, null, null];

        result[0] = jsonCfgFile["Server"]["address"];
        result[1] = jsonCfgFile["Server"]["port"];
        result[2] = jsonCfgFile["Server"]["username"];
        result[3] = jsonCfgFile["Server"]["password"];

        return result;
    }

    module.exports.readOrdreConfig = function() {

        var cfgFile = fs.readFileSync("../web/cfg/ordre.json")

        var jsonCfgFile = JSON.parse(cfgFile);

        var result = jsonCfgFile["Ordre"];      
        
        return result;
    }

}());