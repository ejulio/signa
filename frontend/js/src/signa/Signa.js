;(function(global, undefined)
{
    'use strict';

    global.Signa = {

        camera: {},
        recognizer: {},
        scene: {},

        signalrHub: function()
        {
            return getConnection().signSequence.server;
        },

        initHubs: function()
        {
            return getConnection().hub.start();
        }
    };
})(typeof global === 'undefined' ? window : global);
