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
            
            return Signa.Hubs.sinaisDinamicos()
                .reconhecer(this._buffer.getFrames())
                .then(function(id) {
                    console.log('RECONHECEU O SINAL: ' + id);
                    algoritmoDeSinalDinamico.naoReconheceuFrame();
                    
                    return algoritmoDeSinalDinamico.getSinalId() === (id * 2);
                });
        }
    };

    Signa.reconhecimento.SinalDinamicoReconheceuUltimoFrame = SinalDinamicoReconheceuUltimoFrame;
})(window, window.Signa);
