;(function(window, Signa, undefined) {
    'use strict';

    function SinalDinamicoReconheceuUltimoFrame(algoritmoDeSinalDinamico, buffer) {
        this._algoritmoDeSinalDinamico = algoritmoDeSinalDinamico;
        this._buffer = buffer;
    }

    SinalDinamicoReconheceuUltimoFrame.prototype = {
        _algoritmoDeSinalDinamico: undefined,
        
        reconhecer: function() {
            var algoritmoDeSinalDinamico = this._algoritmoDeSinalDinamico;
            var framesParaReconhecer = this._buffer.getFrames();
            var idSinal = algoritmoDeSinalDinamico.getSinalId();

            return Signa.Hubs.sinaisDinamicos()
                .reconhecer(idSinal, framesParaReconhecer)
                .then(function(sinalReconhecido) {
                    algoritmoDeSinalDinamico.naoReconheceuFrame();
                    
                    return sinalReconhecido;
                });
        }
    };

    Signa.reconhecimento.SinalDinamicoReconheceuUltimoFrame = SinalDinamicoReconheceuUltimoFrame;
})(window, window.Signa);
