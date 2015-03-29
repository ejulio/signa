;(function(window, Signa, undefined) {
    'use strict';

    function SinalDinamico() {
        this.RECONHECENDO = new Signa.reconhecimento.SinalDinamicoReconhecendo(this);
        
        this.NAO_RECONHECEU_FRAME = 
            new Signa.reconhecimento.SinalDinamicoNaoReconheceuFrame(this);
        
        this.RECONHECEU_PRIMEIRO_FRAME = 
            new Signa.reconhecimento.SinalDinamicoReconheceuPrimeiroFrame(this);
        
        this.RECONHECEU_ULTIMO_FRAME = 
            new Signa.reconhecimento.SinalDinamicoReconheceuUltimoFrame(this, this.RECONHECENDO);

        this._estado = this.NAO_RECONHECEU_FRAME;
        this._buffer = this.RECONHECENDO;
        this._comparador = new Signa.frames.Comparador();
    }

    SinalDinamico.prototype = {
        _estado: undefined,
        _buffer: undefined,
        _comparador: undefined,
        _ultimoFrame: undefined,
        _sinalId: -1,

        setSinalId: function(sinalId) {
            this._sinalId = sinalId;
        },

        getSinalId: function() {
            return this._sinalId;
        },

        reconhecer: function(frame) {
            if (this._comparador.framesSaoIguais(frame, this._ultimoFrame)) {
                console.log('MESMO FRAME');
                return Promise.resolve(false);
            }

            var estado = this._estado,
                amostra = [frame];

            this._ultimoFrame = frame;
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

    Signa.reconhecimento.SinalDinamico = SinalDinamico;
})(window, window.Signa);
