;(function(window, Signa, undefined) {
    'use strict';

    function SinalDinamicoReconhecendo(algoritmoDeSinalDinamico) {
        this._frames = [];
        this._algoritmoDeSinalDinamico = algoritmoDeSinalDinamico;
    }

    SinalDinamicoReconhecendo.prototype = {
        _frames: undefined,
        _algoritmoDeSinalDinamico: undefined,

        getFrames: function() {
            return this._frames;
        },  

        reconhecer: function(amostra) {
            this.adicionar(amostra);
            return Promise.resolve(false);
        },

        adicionar: function(amostra) {
            this._frames.push(amostra[0]);
            if (this._frames.length === 50) {
                this._algoritmoDeSinalDinamico.naoReconheceuFrame();
                this._frames = [];
                console.log('LIMPANDO FRAMES - BUFFER CHEIO');
            }
        },

        limpar: function() {
            this._frames = [];
        }
    };

    Signa.reconhecimento.SinalDinamicoReconhecendo = SinalDinamicoReconhecendo;
})(window, window.Signa);
