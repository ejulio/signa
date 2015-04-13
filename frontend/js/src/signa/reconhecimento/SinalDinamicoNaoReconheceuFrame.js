;(function(window, Signa, undefined) {
    'use strict';

    function SinalDinamicoNaoReconheceuFrame(algoritmoDeSinalDinamico) {
        this._algoritmoDeSinalDinamico = algoritmoDeSinalDinamico;
    }

    SinalDinamicoNaoReconheceuFrame.prototype = {
        _algoritmoDeSinalDinamico: undefined,
        
        reconhecer: function(amostra) {
            var algoritmoDeSinalDinamico = this._algoritmoDeSinalDinamico;

            return Signa.Hubs.sinaisDinamicos()
                .reconhecerPrimeiroFrame(amostra)
                .then(function(id) {
                    if (algoritmoDeSinalDinamico.getSinalId() === id) {
                        algoritmoDeSinalDinamico.reconheceuPrimeiroFrame();
                        return false;
                    }
                    algoritmoDeSinalDinamico.naoReconheceuFrame();
                    return false;
                });
        }
    };

    Signa.reconhecimento.SinalDinamicoNaoReconheceuFrame = SinalDinamicoNaoReconheceuFrame;
})(window, window.Signa);