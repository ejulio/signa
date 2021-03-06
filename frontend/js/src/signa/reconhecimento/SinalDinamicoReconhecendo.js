;(function(window, Signa, undefined) {
    'use strict';

    var QUANTIDADE_MAXIMA = 50;

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
            if (this._frames.length === QUANTIDADE_MAXIMA) {
                this._algoritmoDeSinalDinamico.naoReconheceuFrame();
                this._frames = [];
            }
        },

        limpar: function() {
            this._frames = [];
        }
    };

    Signa.reconhecimento.SinalDinamicoReconhecendo = SinalDinamicoReconhecendo;
})(window, window.Signa);
