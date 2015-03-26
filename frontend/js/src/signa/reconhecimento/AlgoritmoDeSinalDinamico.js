;(function(window, Signa, undefined) {
    'use strict';

    function AlgoritmoDeSinalDinamico() {
        this.RECONHECENDO = new Signa.reconhecimento.AlgoritmoDeSinalDinamicoReconhecendo(this);
        
        this.NAO_RECONHECEU_FRAME = 
            new Signa.reconhecimento.AlgoritmoDeSinalDinamicoNaoReconheceuFrame(this);
        
        this.RECONHECEU_PRIMEIRO_FRAME = 
            new Signa.reconhecimento.AlgoritmoDeSinalDinamicoReconheceuPrimeiroFrame(this);
        
        this.RECONHECEU_ULTIMO_FRAME = 
            new Signa.reconhecimento.AlgoritmoDeSinalDinamicoReconheceuUltimoFrame(this, this.RECONHECENDO);

        this._estado = this.NAO_RECONHECEU_FRAME;
        this._buffer = this.RECONHECENDO;
    }

    AlgoritmoDeSinalDinamico.prototype = {
        _estado: undefined,
        _buffer: undefined,
        _sinalId: -1,

        setSinalId: function(sinalId) {
            this._sinalId = sinalId;
        },

        getSinalId: function() {
            return this._sinalId;
        },

        reconhecer: function(frame) {
            var estado = this._estado,
                amostra = [frame];

            this.reconhecendo(amostra);

            return estado.reconhecer(amostra);
        },

        reconhecendo: function(amostra) {
            console.log('RECONHECENDO');
            this._estado = this.RECONHECENDO;
            this._salvarAmostraNoBuffer(amostra);
        },

        _salvarAmostraNoBuffer: function(amostra) {
            this._estado.reconhecer(amostra);
        },

        naoReconheceuFrame: function() {
            console.log('NÃO RECONHECEU');
            this._buffer.limpar();
            this._buffer.desativar();
            this._estado = this.NAO_RECONHECEU_FRAME;
        },

        reconheceuPrimeiroFrame: function() {
            console.log('RECONHECEU');
            this._buffer.ativar();
            this._estado = this.RECONHECEU_PRIMEIRO_FRAME;
        },

        reconheceuUltimoFrame: function() {
            console.log('HORA DE RECONHECER O SINAL');
            this._buffer.desativar();
            this._estado = this.RECONHECEU_ULTIMO_FRAME;
        }
    };

    Signa.reconhecimento.AlgoritmoDeSinalDinamico = AlgoritmoDeSinalDinamico;
})(window, window.Signa);
