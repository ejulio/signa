;(function(global, undefined)
{
    'use strict';

    global.Signa = {

        URL: '',

        camera: {},
        reconhecimento: {},
        cenas: {},
        frames: {},

        montarUrlDoServidor: function(caminho) {
            return 'http://localhost:9000/' + caminho;
        },

        treinarAlgoritmos: function() {
            alert('TESTE');
        }
    };
})(typeof global === 'undefined' ? window : global);
