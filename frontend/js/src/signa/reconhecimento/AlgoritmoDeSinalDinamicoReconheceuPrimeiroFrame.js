;(function(window, Signa, undefined) {
    'use strict';

    function AlgoritmoDeSinalDinamicoReconheceuPrimeiroFrame(algoritmoDeSinalDinamico) {
        this._algoritmoDeSinalDinamico = algoritmoDeSinalDinamico;
    }

    AlgoritmoDeSinalDinamicoReconheceuPrimeiroFrame.prototype = {
        _algoritmoDeSinalDinamico: undefined,

        reconhecer: function(amostra) {
            var algoritmoDeSinalDinamico = this._algoritmoDeSinalDinamico;

            return Signa.Hubs.sinaisDinamicos()
                .reconhecerUltimoFrame(amostra)
                .then(function(id) {
                    if (algoritmoDeSinalDinamico.getSinalId() === id) {
                        algoritmoDeSinalDinamico.reconheceuUltimoFrame();
                        return false;
                    }
                    algoritmoDeSinalDinamico.reconheceuPrimeiroFrame();
                    return false;
                });
        }
    };

    Signa.reconhecimento.AlgoritmoDeSinalDinamicoReconheceuPrimeiroFrame = AlgoritmoDeSinalDinamicoReconheceuPrimeiroFrame;
})(window, window.Signa);
