;(function(window, undefined)
{
    'use strict';
    
    var connection = $.connection;
    connection.hub.url = 'http://localhost:9000/signalr';

    window.Signa = {

        camera: {},
        recognizer: {},
        scene: {},

        HUB: connection.recognizer.server,

        initHubs: function()
        {
            return connection.hub.start();
        }
    };
})(window);
