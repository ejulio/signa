;(function(window, Signa, undefined) {
    'use strict';

    function SinalDinamicoNaoReconheceuFrame(algoritmoDeSinalDinamico) {
        this._algoritmoDeSinalDinamico = algoritmoDeSinalDinamico;
    }

    SinalDinamicoNaoReconheceuFrame.prototype = {
        _algoritmoDeSinalDinamico: undefined,
        
        reconhecer: function(amostraPrimeiroFrame) {
            var algoritmoDeSinalDinamico = this._algoritmoDeSinalDinamico;
            var idSinal = algoritmoDeSinalDinamico.getSinalId();

            return Signa.Hubs.sinaisDinamicos()
                .reconhecerPrimeiroFrame(idSinal, amostraPrimeiroFrame)
                .then(function(reconheceuAmostraComoPrimeiroFrameDoSinal) {
                    if (reconheceuAmostraComoPrimeiroFrameDoSinal) {
                        algoritmoDeSinalDinamico.setAmostraPrimeiroFrame(amostraPrimeiroFrame);
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
