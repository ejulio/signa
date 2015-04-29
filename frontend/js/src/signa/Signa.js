;(function(global, undefined)
{
    'use strict';

    global.Signa = {

        URL: 'http://localhost:9000/',

        camera: {},
        reconhecimento: {},
        cenas: {},
        frames: {},

        montarUrlDoServidor: function(caminho) {
            return this.URL + caminho;
        },

        treinarAlgoritmos: function() {
            return $.post(this.montarUrlDoServidor('sinais/TreinarAlgoritmos'));
        }
    };
})(typeof global === 'undefined' ? window : global);
