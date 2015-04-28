;(function(window, Signa, undefined) {
    'use strict';

    function SinalDinamicoReconheceuPrimeiroFrame(algoritmoDeSinalDinamico) {
        this._algoritmoDeSinalDinamico = algoritmoDeSinalDinamico;
    }

    SinalDinamicoReconheceuPrimeiroFrame.prototype = {
        _algoritmoDeSinalDinamico: undefined,

        reconhecer: function(amostraUltimoFrame) {
            var algoritmoDeSinalDinamico = this._algoritmoDeSinalDinamico;
            var amostraPrimeiroFrame = algoritmoDeSinalDinamico.getAmostraPrimeiroFrame();
            var idSinal = algoritmoDeSinalDinamico.getSinalId(); 

            return Signa.Hubs.sinaisDinamicos()
                .reconhecerUltimoFrame(idSinal, amostraPrimeiroFrame, amostraUltimoFrame)
                .then(function(reconheceuAmostraComoUltimoFrameDoSinal) {
                    if (reconheceuAmostraComoUltimoFrameDoSinal) {
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
