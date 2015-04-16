;(function(window, Signa, undefined) {
    'use strict';

    function SinalDinamicoReconhecendo() {
        this._frames = [];
        this._deveArmazenarOsFrames = false;
    }

    SinalDinamicoReconhecendo.prototype = {
        _frames: undefined,

        getFrames: function() {
            return this._frames;
        },  

        reconhecer: function(amostra) {
            console.log('guardando sinal');
            this._frames.push(amostra[0]);

            return Promise.resolve(false);
        },

        limpar: function() {
            console.log('limpando sinais');
            this._frames = [];
        }
    };

    Signa.reconhecimento.SinalDinamicoReconhecendo = SinalDinamicoReconhecendo;
})(window, window.Signa);
