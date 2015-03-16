;(function(window, Signa, undefined)
{
    'use strict';
    
    var conexao = $.connection;
    conexao.hub.url = Signa.montarUrlDoServidor('signalr');

    Signa.Hubs = {
        iniciar: function()
        {
            return conexao.hub.start();
        },

        sinais: function()
        {
            return conexao.sinais.server;
        },

        sinaisEstaticos: function()
        {
            return conexao.sinaisEstaticos.server;
        },

        sinaisDinamicos: function()
        {
            return conexao.sinaisDinamicos.server;
        }
    };

})(window, window.Signa);
