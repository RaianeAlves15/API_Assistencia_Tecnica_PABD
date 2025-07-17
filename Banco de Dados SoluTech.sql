CREATE DATABASE AssistenciaTecnicaDb;
USE AssistenciaTecnicaDb;

-- =============================================================================
-- TABELAS PRINCIPAIS - TODAS COM ID PADRONIZADO
-- =============================================================================

-- Cliente
CREATE TABLE Clientes (
    Id INT PRIMARY KEY AUTO_INCREMENT,
    NomeCliente VARCHAR(100) NOT NULL,
    CpfCliente VARCHAR(20) NOT NULL,
    RgCliente VARCHAR(20) NOT NULL,
    TelefoneCliente VARCHAR(20) NOT NULL,
    EmailCliente VARCHAR(100) NOT NULL,
    RuaCliente VARCHAR(100) NOT NULL,
    BairroCliente VARCHAR(100) NOT NULL,
    CidadeCliente VARCHAR(100) NOT NULL
);

-- Equipamento (ID PADRONIZADO)
CREATE TABLE Equipamentos (
    Id INT PRIMARY KEY AUTO_INCREMENT,
    NomeEquipamento VARCHAR(100) NOT NULL,
    Fabricante VARCHAR(100) NOT NULL,
    Modelo VARCHAR(100) NOT NULL,
    NumeroDeSerie VARCHAR(100) NOT NULL,
    CodigoDeFabricacao VARCHAR(100) NOT NULL,
    AnoDeFabricacao INT NOT NULL,
    Voltagem VARCHAR(20) NOT NULL,
    Amperagem VARCHAR(20) NOT NULL
);

-- Fornecedor (ID PADRONIZADO)
CREATE TABLE Fornecedores (
    Id INT PRIMARY KEY AUTO_INCREMENT,
    NomeFornecedor VARCHAR(100) NOT NULL,
    CnpjCpf VARCHAR(50) NOT NULL,
    InscricaoEstadual VARCHAR(50) NOT NULL,
    Email VARCHAR(100) NOT NULL,
    Telefone VARCHAR(20) NOT NULL,
    TelefoneCelular VARCHAR(20) NOT NULL,
    NumeroDoImovel VARCHAR(20) NOT NULL,
    Cep VARCHAR(20) NOT NULL,
    Bairro VARCHAR(50) NOT NULL,
    Cidade VARCHAR(50) NOT NULL,
    Estado VARCHAR(50) NOT NULL,
    Pais VARCHAR(50) NOT NULL,
    Site VARCHAR(100),
    Representante VARCHAR(100) NOT NULL
);

-- Peça
CREATE TABLE Pecas (
    Id INT PRIMARY KEY AUTO_INCREMENT,
    NomePeca VARCHAR(100) NOT NULL,
    Fabricante VARCHAR(100) NOT NULL,
    LocalDeFabricacao VARCHAR(100) NOT NULL,
    PesoKg DOUBLE NOT NULL,
    Quantidade INT NOT NULL,
    NumeroDeSerie VARCHAR(100) NOT NULL,
    CodigoDeProducao VARCHAR(100) NOT NULL,
    Preco DECIMAL(10,2) NOT NULL,
    Observacao TEXT NOT NULL
);

-- Orçamento (ID PADRONIZADO)
CREATE TABLE Orcamentos (
    Id INT PRIMARY KEY AUTO_INCREMENT,
    NomeCliente VARCHAR(100) NOT NULL,
    Cpf VARCHAR(20) NOT NULL,
    Rg VARCHAR(20) NOT NULL,
    Telefone VARCHAR(20) NOT NULL,
    Rua VARCHAR(100) NOT NULL,
    Bairro VARCHAR(100) NOT NULL,
    Cidade VARCHAR(100) NOT NULL,
    Cep VARCHAR(20) NOT NULL,
    NomeEquipamento VARCHAR(100) NOT NULL,
    Modelo VARCHAR(100) NOT NULL,
    Fabricante VARCHAR(100) NOT NULL,
    AnoFabricacao INT NOT NULL,
    Voltagem VARCHAR(20) NOT NULL,
    Amperagem VARCHAR(20) NOT NULL,
    Pecas TEXT NOT NULL,
    FormaDePagamento VARCHAR(50) NOT NULL,
    PrazoDeEntrega VARCHAR(50) NOT NULL,
    Observacao TEXT,
    ValorSemDesconto DECIMAL(10,2) NOT NULL,
    ValorComDesconto DECIMAL(10,2) NOT NULL
);

-- Reparo (ID PADRONIZADO)
CREATE TABLE Reparos (
    Id INT PRIMARY KEY AUTO_INCREMENT,
    NomeCliente VARCHAR(100) NOT NULL,
    Cpf VARCHAR(20) NOT NULL,
    Rg VARCHAR(20) NOT NULL,
    Telefone VARCHAR(20) NOT NULL,
    Rua VARCHAR(100) NOT NULL,
    Bairro VARCHAR(100) NOT NULL,
    Cidade VARCHAR(100) NOT NULL,
    Cep VARCHAR(20) NOT NULL,
    NomeEquipamento VARCHAR(100) NOT NULL,
    Modelo VARCHAR(100) NOT NULL,
    Fabricante VARCHAR(100) NOT NULL,
    AnoFabricacao INT NOT NULL,
    Voltagem VARCHAR(20) NOT NULL,
    Amperagem VARCHAR(20) NOT NULL,
    Pecas TEXT NOT NULL,
    FormaDePagamento VARCHAR(50) NOT NULL,
    PrazoDeEntrega VARCHAR(50) NOT NULL,
    Observacao TEXT,
    ValorSemDesconto DECIMAL(10,2) NOT NULL,
    ValorComDesconto DECIMAL(10,2) NOT NULL
);

-- =============================================================================
-- TABELAS DE RELACIONAMENTO N:N - CHAVES COMPOSTAS (SEM IDs AUXILIARES)
-- =============================================================================

-- Orcamento × Peca (N:N)
CREATE TABLE OrcamentoPecas (
    OrcamentoId INT NOT NULL,
    PecaId INT NOT NULL,
    Quantidade INT NOT NULL,
    PrecoUnitario DECIMAL(10,2) NOT NULL,
    PRIMARY KEY (OrcamentoId, PecaId),
    FOREIGN KEY (OrcamentoId) REFERENCES Orcamentos(Id) ON DELETE CASCADE,
    FOREIGN KEY (PecaId) REFERENCES Pecas(Id) ON DELETE CASCADE
);

-- Reparo × Equipamento (N:N)
CREATE TABLE ReparoEquipamentos (
    ReparoId INT NOT NULL,
    EquipamentoId INT NOT NULL,
    PRIMARY KEY (ReparoId, EquipamentoId),
    FOREIGN KEY (ReparoId) REFERENCES Reparos(Id) ON DELETE CASCADE,
    FOREIGN KEY (EquipamentoId) REFERENCES Equipamentos(Id) ON DELETE CASCADE
);

-- Fornecedor × Peca (N:N)
CREATE TABLE FornecedorPecas (
    FornecedorId INT NOT NULL,
    PecaId INT NOT NULL,
    PrecoUnitario DECIMAL(10,2) NOT NULL,
    DataUltimaCompra DATE NOT NULL,
    PRIMARY KEY (FornecedorId, PecaId),
    FOREIGN KEY (FornecedorId) REFERENCES Fornecedores(Id) ON DELETE CASCADE,
    FOREIGN KEY (PecaId) REFERENCES Pecas(Id) ON DELETE CASCADE
);



select*from orcamentos;