;(function(window, Signa, undefined)
{
    'use strict';
    
    var connection = $.connection;
    connection.hub.url = 'http://localhost:9000/signalr';

    Signa.Hubs = {
        init: function()
        {
            return connection.hub.start();
        },

        signSequence: function()
        {
            return connection.signSequence.server;
        },

        staticSignRecognizer: function()
        {
            return connection.staticSignRecognizer.server;
        },

        dynamicSignRecognizer: function()
        {
            return connection.dynamicSignRecognizer.server;
        }
    };

})(window, window.Signa);
