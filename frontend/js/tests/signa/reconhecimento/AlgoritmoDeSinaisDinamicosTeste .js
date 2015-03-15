var FrameBuilder = require('../../builders/FrameBuilder.js'),
    MaoBuilder = require('../../builders/MaoBuilder.js'),
    DedoBuilder = require('../../builders/DedoBuilder.js');

global.THREE = require('three');
require('../../../src/signa/Signa.js');
require('../../../src/signa/reconhecimento/AlgoritmoDeSinaisDinamicos.js');
require('../../../src/signa/reconhecimento/InformacoesDoFrame.js');

describe('AlgoritmoDeSinaisDinamicos', function() {
    var DIRECAO_MAO_ESQUERDA = [1, 2, 3],
        VETOR_NORMAL_PALMA_MAO_ESQUERDA = [0, 0, 1],
        DIRECAO_MAO_DIREITA = [4, 5, 6],
        VETOR_NORMAL_PALMA_MAO_DIREITA = [1, 1, 0];

    var algoritmoDeSinaisDinamicos;
    var informacoesDoFrame;
    beforeEach(function() {
        algoritmoDeSinaisDinamicos = new Signa.reconhecimento.AlgoritmoDeSinaisDinamicos();
        informacoesDoFrame = new Signa.reconhecimento.InformacoesDoFrame();

        Signa.Hubs = {
            iniciar: jasmine.createSpy('Signa.Hubs.iniciar'),
            sinaisDinamicos: jasmine.createSpy('Signa.Hubs.sinaisDinamicos')
        };

        Signa.Hubs.iniciar.and.returnValue({
            then: function(callback) { callback(); }
        });

        Signa.Hubs.sinaisDinamicos.and.returnValue({
            reconhecer: jasmine.createSpy('Signa.Hubs.sinaisDinamicos.reconhecer'),
            reconhecerPrimeiroFrame: jasmine.createSpy('Signa.Hubs.sinaisDinamicos.reconhecerPrimeiroFrame'),
            reconhecerUltimoFrame: jasmine.createSpy('Signa.Hubs.sinaisDinamicos.reconhecerUltimoFrame')
        });

        Signa.Hubs.sinaisDinamicos().reconhecer.and,returnValue({
            then: function(callback) { callback(); }
        });

        Signa.Hubs.sinaisDinamicos().reconhecerUltimoFrame.and,returnValue({
            then: function(callback) { callback(); }
        });

        Signa.Hubs.sinaisDinamicos().reconhecerPrimeiroFrame.and,returnValue({
            then: function(callback) { callback(); }
        });
    });

    it('reconhecendo um sinal dinamico', function() {
        var frames = dadaUmaColecaoDeFrames();
        var reconhecerSinalCallback = jasmine.createSpy('reconhecerSinalCallback');

        algoritmoDeSinaisDinamicos.onReconhecerSinal(reconhecerSinalCallback);

        for (var i = 0; i < frames.length; i++) {
            var frameDaAmostra = informacoesDoFrame.extrairParaAmostra(frame);
            algoritmoDeSinaisDinamicos.reconhecer(frameDaAmostra);
        }
        
        expect(reconhecerSinalCallback).toHaveBeenCalled();
        expect(frameDaAmostra.MaoDireita).toBeNull();
        deveRetornarUmFrameDaAmostraComDadosDoLeapFrame(frameDaAmostra.MaoEsquerda, maoEsquerda);
    });

    function dadaUmaMaoEsquerda() {
        var dedos = [
            DedoBuilder.dedao(),
            DedoBuilder.indicador(),
            DedoBuilder.meio(),
            DedoBuilder.anelar(),
            DedoBuilder.mindinho()
        ];

        return new MaoBuilder()
            .comDirecao(DIRECAO_MAO_ESQUERDA)
            .comVetorNormalDaPalma(VETOR_NORMAL_PALMA_MAO_ESQUERDA)
            .comDedos(dedos)
            .construir();
    }

    function dadaUmaMaoDireita() {
        var dedos = [
            DedoBuilder.dedao(),
            DedoBuilder.indicador(),
            DedoBuilder.meio(),
            DedoBuilder.anelar(),
            DedoBuilder.mindinho()
        ];

        return new MaoBuilder()
            .comDirecao(DIRECAO_MAO_DIREITA)
            .comVetorNormalDaPalma(VETOR_NORMAL_PALMA_MAO_DIREITA)
            .comDedos(dedos)
            .construir();
    }

    function dadoUmFrameComMaos(maoEsquerda, maoDireita) {
        return new FrameBuilder()
            .comMaoEsquerda(maoEsquerda)
            .comMaoDireita(maoDireita)
            .construir();
    }

    function deveRetornarUmFrameDaAmostraComDadosDoLeapFrame(frameDaAmostra, mao) {
        expect(frameDaAmostra).not.toBeNull();
        expect(frameDaAmostra.VetorNormalDaPalma).toBe(mao.palmNormal);
        expect(frameDaAmostra.Direcao).toBe(mao.direction);
        
        var dedos = frameDaAmostra.Dedos;        
        for (var i = 0; i < dedos.length; i++)
        {
            expect(dedos[i].Tipo).toBe(mao.fingers[i].type);
            expect(dedos[i].Direcao).toBe(mao.fingers[i].direction);
        }
    }
});
