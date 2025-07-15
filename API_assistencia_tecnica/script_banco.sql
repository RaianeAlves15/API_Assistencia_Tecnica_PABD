DROP DATABASE IF EXISTS assistencia_tecnica;
CREATE DATABASE assistencia_tecnica CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci;
USE assistencia_tecnica;

-- Clientes
CREATE TABLE Clientes (
    Id INT PRIMARY KEY AUTO_INCREMENT,
    NomeCliente VARCHAR(255) NOT NULL,
    CpfCliente VARCHAR(14) NOT NULL UNIQUE,
    RgCliente VARCHAR(20) NOT NULL,
    TelefoneCliente VARCHAR(20) NOT NULL,
    EmailCliente VARCHAR(255) NOT NULL,
    RuaCliente VARCHAR(255) NOT NULL,
    BairroCliente VARCHAR(100) NOT NULL,
    CidadeCliente VARCHAR(100) NOT NULL
);

-- Equipamentos
CREATE TABLE Equipamentos (
    Id INT PRIMARY KEY AUTO_INCREMENT,
    NomeEquipamento VARCHAR(255) NOT NULL,
    Fabricante VARCHAR(100) NOT NULL,
    Modelo VARCHAR(100) NOT NULL,
    NumeroDeSerie VARCHAR(100) NOT NULL UNIQUE,
    CodigoDeFabricacao VARCHAR(100) NOT NULL,
    AnoDeFabricacao INT NOT NULL,
    Voltagem VARCHAR(20) NOT NULL,
    Amperagem VARCHAR(20) NOT NULL
);

-- Fornecedores
CREATE TABLE Fornecedores (
    Id INT PRIMARY KEY AUTO_INCREMENT,
    NomeFornecedor VARCHAR(255) NOT NULL,
    CnpjCpf VARCHAR(18) NOT NULL UNIQUE,
    InscricaoEstadual VARCHAR(20) NOT NULL,
    Email VARCHAR(255) NOT NULL,
    Telefone VARCHAR(20) NOT NULL,
    TelefoneCelular VARCHAR(20) NOT NULL,
    NumeroDoImovel VARCHAR(20) NOT NULL,
    Cep VARCHAR(10) NOT NULL,
    Bairro VARCHAR(100) NOT NULL,
    Cidade VARCHAR(100) NOT NULL,
    Estado VARCHAR(50) NOT NULL,
    Pais VARCHAR(50) NOT NULL,
    Site VARCHAR(255) NULL,
    Representante VARCHAR(255) NOT NULL
);

-- Pecas
CREATE TABLE Pecas (
    Id INT PRIMARY KEY AUTO_INCREMENT,
    NomePeca VARCHAR(255) NOT NULL,
    Fabricante VARCHAR(100) NOT NULL,
    LocalDeFabricacao VARCHAR(100) NOT NULL,
    PesoKg DOUBLE NOT NULL,
    Quantidade INT NOT NULL,
    NumeroDeSerie VARCHAR(100) NOT NULL,
    CodigoDeProducao VARCHAR(100) NOT NULL UNIQUE,
    Preco DECIMAL(10,2) NOT NULL,
    Observacao TEXT NOT NULL
);

-- Orcamentos (NOVA ESTRUTURA - sem duplicação)
CREATE TABLE Orcamentos (
    Id INT PRIMARY KEY AUTO_INCREMENT,
    ClienteId INT NOT NULL,
    EquipamentoId INT NOT NULL,
    FormaDePagamento VARCHAR(100) NOT NULL,
    PrazoDeEntrega VARCHAR(100) NOT NULL,
    Observacao TEXT NULL,
    ValorSemDesconto DECIMAL(10,2) NOT NULL,
    ValorComDesconto DECIMAL(10,2) NOT NULL,
    DataCriacao TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    DataAprovacao TIMESTAMP NULL,
    Status VARCHAR(20) DEFAULT 'Pendente',
    
    FOREIGN KEY (ClienteId) REFERENCES Clientes(Id),
    FOREIGN KEY (EquipamentoId) REFERENCES Equipamentos(Id)
);

-- Reparos (NOVA ESTRUTURA - sem duplicação)
CREATE TABLE Reparos (
    Id INT PRIMARY KEY AUTO_INCREMENT,
    ClienteId INT NOT NULL,
    EquipamentoId INT NOT NULL,
    OrcamentoId INT NULL,
    FormaDePagamento VARCHAR(100) NOT NULL,
    PrazoDeEntrega VARCHAR(100) NOT NULL,
    Observacao TEXT NULL,
    Diagnostico TEXT NULL,
    SolucaoAplicada TEXT NULL,
    ValorSemDesconto DECIMAL(10,2) NOT NULL,
    ValorComDesconto DECIMAL(10,2) NOT NULL,
    DataInicio TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    DataConclusao TIMESTAMP NULL,
    DataEntrega TIMESTAMP NULL,
    Status VARCHAR(20) DEFAULT 'Iniciado',
    
    FOREIGN KEY (ClienteId) REFERENCES Clientes(Id),
    FOREIGN KEY (EquipamentoId) REFERENCES Equipamentos(Id),
    FOREIGN KEY (OrcamentoId) REFERENCES Orcamentos(Id)
);

-- Relacionamentos N:N
CREATE TABLE OrcamentoPecas (
    OrcamentoId INT NOT NULL,
    PecaId INT NOT NULL,
    Quantidade INT NOT NULL,
    PrecoUnitario DECIMAL(10,2) NOT NULL,
    PRIMARY KEY (OrcamentoId, PecaId),
    FOREIGN KEY (OrcamentoId) REFERENCES Orcamentos(Id) ON DELETE CASCADE,
    FOREIGN KEY (PecaId) REFERENCES Pecas(Id)
);

CREATE TABLE ReparoPecas (
    ReparoId INT NOT NULL,
    PecaId INT NOT NULL,
    Quantidade INT NOT NULL,
    PrecoUnitario DECIMAL(10,2) NOT NULL,
    PecaUtilizada BOOLEAN DEFAULT FALSE,
    PRIMARY KEY (ReparoId, PecaId),
    FOREIGN KEY (ReparoId) REFERENCES Reparos(Id) ON DELETE CASCADE,
    FOREIGN KEY (PecaId) REFERENCES Pecas(Id)
);

CREATE TABLE ReparoEquipamentos (
    ReparoId INT NOT NULL,
    EquipamentoId INT NOT NULL,
    PRIMARY KEY (ReparoId, EquipamentoId),
    FOREIGN KEY (ReparoId) REFERENCES Reparos(Id) ON DELETE CASCADE,
    FOREIGN KEY (EquipamentoId) REFERENCES Equipamentos(Id)
);

CREATE TABLE FornecedorPecas (
    FornecedorId INT NOT NULL,
    PecaId INT NOT NULL,
    PrecoUnitario DECIMAL(10,2) NOT NULL,
    DataUltimaCompra TIMESTAMP NULL,
    PRIMARY KEY (FornecedorId, PecaId),
    FOREIGN KEY (FornecedorId) REFERENCES Fornecedores(Id) ON DELETE CASCADE,
    FOREIGN KEY (PecaId) REFERENCES Pecas(Id)
);

-- Dados de exemplo
INSERT INTO Clientes (NomeCliente, CpfCliente, RgCliente, TelefoneCliente, EmailCliente, RuaCliente, BairroCliente, CidadeCliente) VALUES
('João Silva', '111.111.111-11', '11.111.111-1', '(11) 99999-1111', 'joao@email.com', 'Rua das Flores, 123', 'Centro', 'São Paulo'),
('Maria Santos', '222.222.222-22', '22.222.222-2', '(11) 88888-2222', 'maria@email.com', 'Av. Principal, 456', 'Jardim', 'Rio de Janeiro'),
('Pedro Costa', '333.333.333-33', '33.333.333-3', '(11) 77777-3333', 'pedro@email.com', 'Rua da Paz, 789', 'Vila Nova', 'Belo Horizonte');

INSERT INTO Equipamentos (NomeEquipamento, Fabricante, Modelo, NumeroDeSerie, CodigoDeFabricacao, AnoDeFabricacao, Voltagem, Amperagem) VALUES
('Geladeira', 'Samsung', 'RF23R', 'SER001', 'COD001', 2022, '220V', '10A'),
('Micro-ondas', 'LG', 'MS2355R', 'SER002', 'COD002', 2023, '110V', '15A'),
('Máquina de Lavar', 'Brastemp', 'BWL11A', 'SER003', 'COD003', 2021, '220V', '20A');

INSERT INTO Fornecedores (NomeFornecedor, CnpjCpf, InscricaoEstadual, Email, Telefone, TelefoneCelular, NumeroDoImovel, Cep, Bairro, Cidade, Estado, Pais, Site, Representante) VALUES
('TecPeças Ltda', '11.111.111/0001-11', '111.111.111.111', 'contato@tecpecas.com', '(11) 3333-1111', '(11) 99999-1111', '100', '01000-000', 'Industrial', 'São Paulo', 'SP', 'Brasil', 'www.tecpecas.com', 'Carlos Técnico'),
('Eletrônicos SA', '22.222.222/0001-22', '222.222.222.222', 'vendas@eletronicos.com', '(21) 3333-2222', '(21) 99999-2222', '200', '20000-000', 'Comercial', 'Rio de Janeiro', 'RJ', 'Brasil', 'www.eletronicos.com', 'Ana Vendas');

INSERT INTO Pecas (NomePeca, Fabricante, LocalDeFabricacao, PesoKg, Quantidade, NumeroDeSerie, CodigoDeProducao, Preco, Observacao) VALUES
('Compressor Geladeira', 'Samsung', 'Coreia do Sul', 2.5, 10, 'COMP001', 'PROD001', 350.00, 'Compressor original Samsung'),
('Magnetron Micro-ondas', 'LG', 'China', 0.8, 5, 'MAG001', 'PROD002', 180.00, 'Magnetron 2M213'),
('Motor Lavadora', 'Brastemp', 'Brasil', 3.2, 8, 'MOT001', 'PROD003', 220.00, 'Motor 220V');

INSERT INTO Orcamentos (ClienteId, EquipamentoId, FormaDePagamento, PrazoDeEntrega, Observacao, ValorSemDesconto, ValorComDesconto, Status) VALUES
(1, 1, 'Cartão de Crédito', '5 dias úteis', 'Troca de compressor', 450.00, 420.00, 'Pendente'),
(2, 2, 'PIX', '3 dias úteis', 'Reparo no magnetron', 250.00, 230.00, 'Aprovado'),
(3, 3, 'Dinheiro', '7 dias úteis', 'Troca do motor', 320.00, 300.00, 'Pendente');

INSERT INTO Reparos (ClienteId, EquipamentoId, OrcamentoId, FormaDePagamento, PrazoDeEntrega, Observacao, Diagnostico, SolucaoAplicada, ValorSemDesconto, ValorComDesconto, Status) VALUES
(2, 2, 2, 'PIX', '3 dias úteis', 'Reparo no magnetron', 'Magnetron queimado', 'Substituição do magnetron', 250.00, 230.00, 'Concluido');

INSERT INTO OrcamentoPecas (OrcamentoId, PecaId, Quantidade, PrecoUnitario) VALUES
(1, 1, 1, 350.00),
(2, 2, 1, 180.00),
(3, 3, 1, 220.00);

INSERT INTO ReparoPecas (ReparoId, PecaId, Quantidade, PrecoUnitario, PecaUtilizada) VALUES
(1, 2, 1, 180.00, TRUE);

SELECT 'Banco criado com sucesso!' as Status;
SELECT 'Clientes' as Tabela, COUNT(*) as Total FROM Clientes
UNION ALL
SELECT 'Equipamentos', COUNT(*) FROM Equipamentos
UNION ALL
SELECT 'Orcamentos', COUNT(*) FROM Orcamentos
UNION ALL
SELECT 'Reparos', COUNT(*) FROM Reparos;
