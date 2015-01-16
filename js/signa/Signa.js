;(function(window, undefined)
{
    var connection = $.connection;
    connection.hub.url = 'http://localhost:9000/signalr';

    window.Signa = {

        HUB: connection.recognizer.server,

        initHubs: function()
        {
            return connection.hub.start();
        }
    };
})(window);
