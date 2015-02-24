;(function(global, undefined)
{
    'use strict';

    var connection;
    function getConnection()
    {
        if (!connection)
        {
            connection = $.connection;
            connection.hub.url = 'http://localhost:9000/signalr';
        }

        return connection;
    }

    global.Signa = {

        camera: {},
        recognizer: {},
        scene: {},

        signalrHub: function()
        {
            return getConnection().recognizer.server;
        },

        initHubs: function()
        {
            return getConnection().hub.start();
        }
    };
})(typeof global === 'undefined' ? window : global);
