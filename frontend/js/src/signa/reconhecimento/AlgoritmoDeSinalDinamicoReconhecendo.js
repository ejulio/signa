;(function(window, Signa, undefined) {
    'use strict';

    function AlgoritmoDeSinalDinamicoReconhecendo() {
        this._frames = [];
        this._deveArmazenarOsFrames = false;
    }

    AlgoritmoDeSinalDinamicoReconhecendo.prototype = {
        _frames: undefined,
        _deveArmazenarOsFrames: false,

        getFrames: function() {
            return this._frames;
        },  

        reconhecer: function(amostra) {
            if (this._deveArmazenarOsFrames) {
                this._frames.push(amostra[0]);
            }

            return Promise.resolve(false);
        },

        ativar: function() {
            this._deveArmazenarOsFrames = true;
        },

        desativar: function() {
            this._deveArmazenarOsFrames = false;
        },

        limpar: function() {
            this._frames = [];
        }
    };

    Signa.reconhecimento.AlgoritmoDeSinalDinamicoReconhecendo = AlgoritmoDeSinalDinamicoReconhecendo;
})(window, window.Signa);
