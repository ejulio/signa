;(function(window, Signa, undefined) {
    'use strict';

    function AlgoritmoDeSinalDinamicoReconheceuUltimoFrame(algoritmoDeSinalDinamico, buffer) {
        this._algoritmoDeSinalDinamico = algoritmoDeSinalDinamico;
        this._buffer = buffer;
    }

    AlgoritmoDeSinalDinamicoReconheceuUltimoFrame.prototype = {
        _algoritmoDeSinalDinamico: undefined,
        
        reconhecer: function() {
            var algoritmoDeSinalDinamico = this._algoritmoDeSinalDinamico;
            
            return Signa.Hubs.sinaisDinamicos()
                .reconhecer(this._buffer.getFrames())
                .then(function(id) {
                    algoritmoDeSinalDinamico.naoReconheceuFrame();
                    
                    return algoritmoDeSinalDinamico.getSinalId() === id;
                });
        }
    };

    Signa.reconhecimento.AlgoritmoDeSinalDinamicoReconheceuUltimoFrame = AlgoritmoDeSinalDinamicoReconheceuUltimoFrame;
})(window, window.Signa);
