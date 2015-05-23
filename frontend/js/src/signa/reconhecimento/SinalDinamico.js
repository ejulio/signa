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
        _amostraPrimeiroFrame: undefined,

        setAmostraPrimeiroFrame: function(amostra) {
            this._amostraPrimeiroFrame = amostra;
        },

        getAmostraPrimeiroFrame: function() {
            return this._amostraPrimeiroFrame;
        },

        setSinalId: function(sinalId) {
            console.log('SINAL ' + sinalId);
            this._sinalId = sinalId;
        },

        getSinalId: function() {
            return this._sinalId;
        },

        reconhecer: function(frame) {
            if (this._comparador.framesSaoIguais(frame, this._ultimoFrame)) {
                return Promise.resolve(false);
            }

            var estado = this._estado,
                amostra = [frame];

            this._ultimoFrame = frame;
            this.reconhecendo(amostra);

            return estado.reconhecer(amostra);
        },

        reconhecendo: function(amostra) {
            this._estado = this.RECONHECENDO;
            this._buffer.adicionar(amostra);
        },

        naoReconheceuFrame: function() {
            this._buffer.limpar();
            this._estado = this.NAO_RECONHECEU_FRAME;
        },

        reconheceuPrimeiroFrame: function() {
            this._estado = this.RECONHECEU_PRIMEIRO_FRAME;
        },

        reconheceuUltimoFrame: function() {
            this._estado = this.RECONHECEU_ULTIMO_FRAME;
        }
    };

    Signa.reconhecimento.SinalDinamico = SinalDinamico;
})(window, window.Signa);
