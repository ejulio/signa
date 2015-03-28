;(function(window, Signa, undefined) {
    'use strict';

    function SinalDinamicoReconhecendo() {
        this._frames = [];
        this._deveArmazenarOsFrames = false;
    }

    SinalDinamicoReconhecendo.prototype = {
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

    Signa.reconhecimento.SinalDinamicoReconhecendo = SinalDinamicoReconhecendo;
})(window, window.Signa);
