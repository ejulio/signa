;(function(window, Signa, undefined) {
    'use strict';

    function SinalDinamicoReconheceuPrimeiroFrame(algoritmoDeSinalDinamico) {
        this._algoritmoDeSinalDinamico = algoritmoDeSinalDinamico;
    }

    SinalDinamicoReconheceuPrimeiroFrame.prototype = {
        _algoritmoDeSinalDinamico: undefined,

        reconhecer: function(amostra) {
            var algoritmoDeSinalDinamico = this._algoritmoDeSinalDinamico;

            return Signa.Hubs.sinaisDinamicos()
                .reconhecerUltimoFrame(algoritmoDeSinalDinamico.getAmostraPrimeiroFrame(), amostra)
                .then(function(id) {
                    console.log('ID ÚLTIMO FRAME: ' + id);
                    if ((algoritmoDeSinalDinamico.getSinalId() + 1) === id) {
                        algoritmoDeSinalDinamico.reconheceuUltimoFrame();
                        return false;
                    }
                    algoritmoDeSinalDinamico.reconheceuPrimeiroFrame();
                    return false;
                });
        }
    };

    Signa.reconhecimento.SinalDinamicoReconheceuPrimeiroFrame = SinalDinamicoReconheceuPrimeiroFrame;
})(window, window.Signa);
