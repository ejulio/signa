;(function(global, undefined)
{
    'use strict';

    global.Signa = {

        URL: '',

        camera: {},
        reconhecimento: {},
        cena: {},

        montarUrlDoServidor: function(caminho) {
            return 'http://localhost:9000/' + caminho;
        }
    };
})(typeof global === 'undefined' ? window : global);
