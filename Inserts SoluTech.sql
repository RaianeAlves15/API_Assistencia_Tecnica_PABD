USE AssistenciaTecnicaDb;

-- =============================================================================
-- DADOS DE EXEMPLO - INSERÇÃO NAS TABELAS PRINCIPAIS
-- =============================================================================

-- Clientes
INSERT INTO Clientes (NomeCliente, CpfCliente, RgCliente, TelefoneCliente, EmailCliente, RuaCliente, BairroCliente, CidadeCliente) VALUES
('João Silva Santos', '123.456.789-01', '12.345.678-9', '(11) 98765-4321', 'joao.silva@email.com', 'Rua das Flores, 123', 'Centro', 'São Paulo'),
('Maria Oliveira Costa', '987.654.321-02', '98.765.432-1', '(11) 99876-5432', 'maria.oliveira@email.com', 'Av. Principal, 456', 'Jardim das Rosas', 'São Paulo'),
('Carlos Mendes Lima', '456.789.123-03', '45.678.912-3', '(11) 97654-3210', 'carlos.mendes@email.com', 'Rua do Comércio, 789', 'Vila Nova', 'São Paulo'),
('Ana Paula Ferreira', '321.654.987-04', '32.165.498-7', '(11) 96543-2109', 'ana.ferreira@email.com', 'Rua da Paz, 321', 'Bela Vista', 'São Paulo'),
('Roberto Silva Nunes', '111.222.333-05', '11.222.333-4', '(11) 95432-1098', 'roberto.nunes@email.com', 'Av. Paulista, 1000', 'Bela Vista', 'São Paulo');

-- Equipamentos
INSERT INTO Equipamentos (NomeEquipamento, Fabricante, Modelo, NumeroDeSerie, CodigoDeFabricacao, AnoDeFabricacao, Voltagem, Amperagem) VALUES
('Televisor LED 55"', 'Samsung', 'UN55AU7700', 'SN123456789', 'CF001-2023', 2023, '110V', '1.5A'),
('Geladeira Frost Free', 'Brastemp', 'BRM54HK', 'SN987654321', 'CF002-2022', 2022, '220V', '2.0A'),
('Micro-ondas', 'Panasonic', 'NN-ST67H', 'SN456789123', 'CF003-2023', 2023, '110V', '12A'),
('Ar Condicionado Split', 'LG', 'S4-Q12JA3WF', 'SN321654987', 'CF004-2021', 2021, '220V', '5.5A'),
('Notebook Gamer', 'Dell', 'G15-5520', 'SN789123456', 'CF005-2023', 2023, '110V', '3.5A'),
('Smartphone', 'Apple', 'iPhone 14', 'SN654321987', 'CF006-2022', 2022, '5V', '2.4A');

-- Fornecedores
INSERT INTO Fornecedores (NomeFornecedor, CnpjCpf, InscricaoEstadual, Email, Telefone, TelefoneCelular, NumeroDoImovel, Cep, Bairro, Cidade, Estado, Pais, Site, Representante) VALUES
('Distribuidora Tech Parts Ltda', '12.345.678/0001-90', '123.456.789.012', 'vendas@techparts.com.br', '(11) 3456-7890', '(11) 99123-4567', '1500', '01234-567', 'Industrial', 'São Paulo', 'SP', 'Brasil', 'www.techparts.com.br', 'Roberto Silva'),
('Eletrônicos São Paulo S.A.', '98.765.432/0001-10', '987.654.321.098', 'comercial@eletronicossp.com.br', '(11) 2345-6789', '(11) 98765-4321', '2800', '04567-890', 'Mooca', 'São Paulo', 'SP', 'Brasil', 'www.eletronicossp.com.br', 'Fernanda Costa'),
('Componentes & Cia', '45.678.912/0001-34', '456.789.123.456', 'atendimento@componentescia.com.br', '(11) 3789-0123', '(11) 97654-3210', '950', '02345-678', 'Brás', 'São Paulo', 'SP', 'Brasil', 'www.componentescia.com.br', 'Carlos Mendes'),
('Peças Express Brasil', '32.165.498/0001-76', '321.654.987.321', 'suporte@pecasexpress.com.br', '(11) 4567-8901', '(11) 96543-2109', '1200', '03456-789', 'Bom Retiro', 'São Paulo', 'SP', 'Brasil', 'www.pecasexpress.com.br', 'Ana Rodrigues'),
('GlobalTech Import', '55.666.777/0001-88', '555.666.777.888', 'importacao@globaltech.com.br', '(11) 3333-4444', '(11) 94444-5555', '777', '05678-901', 'Vila Olímpia', 'São Paulo', 'SP', 'Brasil', 'www.globaltech.com.br', 'Lucas Oliveira');

-- Peças
INSERT INTO Pecas (NomePeca, Fabricante, LocalDeFabricacao, PesoKg, Quantidade, NumeroDeSerie, CodigoDeProducao, Preco, Observacao) VALUES
('Tela LCD 55 polegadas', 'Samsung', 'Coreia do Sul', 8.5, 10, 'LCD55001', 'PROD-LCD-001', 450.00, 'Tela para TV LED Samsung 55 polegadas, nova, com garantia de 6 meses'),
('Compressor Geladeira', 'Embraco', 'Brasil', 12.3, 5, 'COMP001', 'PROD-COMP-001', 280.00, 'Compressor para geladeira Brastemp, recondicionado, garantia 3 meses'),
('Magnetron Micro-ondas', 'Panasonic', 'Japão', 2.1, 8, 'MAG001', 'PROD-MAG-001', 120.00, 'Magnetron original Panasonic, novo, garantia 1 ano'),
('Evaporadora Split 12000 BTU', 'LG', 'China', 15.7, 3, 'EVAP001', 'PROD-EVAP-001', 650.00, 'Evaporadora para ar condicionado split LG 12000 BTU, nova'),
('Bateria Notebook', 'Dell', 'China', 0.8, 15, 'BAT001', 'PROD-BAT-001', 180.00, 'Bateria para notebook Dell G15, 6 células, garantia 1 ano'),
('Tela Touch iPhone', 'Apple', 'China', 0.2, 20, 'TOUCH001', 'PROD-TOUCH-001', 320.00, 'Tela touch original iPhone 14, com garantia de 3 meses'),
('Placa Mãe Desktop', 'ASUS', 'Taiwan', 1.2, 7, 'PLACA001', 'PROD-PLACA-001', 550.00, 'Placa mãe ASUS B450M, socket AM4, nova'),
('Fonte ATX 500W', 'Corsair', 'China', 2.5, 12, 'FONTE001', 'PROD-FONTE-001', 280.00, 'Fonte ATX 500W 80+ Bronze, modular, garantia 2 anos');

-- Orçamentos
INSERT INTO Orcamentos (NomeCliente, Cpf, Rg, Telefone, Rua, Bairro, Cidade, Cep, NomeEquipamento, Modelo, Fabricante, AnoFabricacao, Voltagem, Amperagem, Pecas, FormaDePagamento, PrazoDeEntrega, Observacao, ValorSemDesconto, ValorComDesconto) VALUES
('João Silva Santos', '123.456.789-01', '12.345.678-9', '(11) 98765-4321', 'Rua das Flores, 123', 'Centro', 'São Paulo', '01234-567', 'Televisor LED 55"', 'UN55AU7700', 'Samsung', 2023, '110V', '1.5A', 'Tela LCD 55 polegadas', 'Cartão de Crédito', '7 dias úteis', 'Troca da tela devido a impacto', 500.00, 450.00),
('Maria Oliveira Costa', '987.654.321-02', '98.765.432-1', '(11) 99876-5432', 'Av. Principal, 456', 'Jardim das Rosas', 'São Paulo', '04567-890', 'Geladeira Frost Free', 'BRM54HK', 'Brastemp', 2022, '220V', '2.0A', 'Compressor Geladeira', 'PIX', '10 dias úteis', 'Compressor queimado, necessário substituição', 350.00, 320.00),
('Carlos Mendes Lima', '456.789.123-03', '45.678.912-3', '(11) 97654-3210', 'Rua do Comércio, 789', 'Vila Nova', 'São Paulo', '02345-678', 'Micro-ondas', 'NN-ST67H', 'Panasonic', 2023, '110V', '12A', 'Magnetron Micro-ondas', 'Dinheiro', '5 dias úteis', 'Magnetron com defeito, aquecimento irregular', 180.00, 150.00),
('Ana Paula Ferreira', '321.654.987-04', '32.165.498-7', '(11) 96543-2109', 'Rua da Paz, 321', 'Bela Vista', 'São Paulo', '03456-789', 'Ar Condicionado Split', 'S4-Q12JA3WF', 'LG', 2021, '220V', '5.5A', 'Evaporadora Split 12000 BTU', 'Cartão de Débito', '15 dias úteis', 'Evaporadora com vazamento de gás', 750.00, 700.00),
('Roberto Silva Nunes', '111.222.333-05', '11.222.333-4', '(11) 95432-1098', 'Av. Paulista, 1000', 'Bela Vista', 'São Paulo', '01310-100', 'Notebook Gamer', 'G15-5520', 'Dell', 2023, '110V', '3.5A', 'Bateria Notebook', 'Cartão de Crédito', '5 dias úteis', 'Bateria não segura carga, substituição necessária', 220.00, 200.00);

-- Reparos
INSERT INTO Reparos (NomeCliente, Cpf, Rg, Telefone, Rua, Bairro, Cidade, Cep, NomeEquipamento, Modelo, Fabricante, AnoFabricacao, Voltagem, Amperagem, Pecas, FormaDePagamento, PrazoDeEntrega, Observacao, ValorSemDesconto, ValorComDesconto) VALUES
('João Silva Santos', '123.456.789-01', '12.345.678-9', '(11) 98765-4321', 'Rua das Flores, 123', 'Centro', 'São Paulo', '01234-567', 'Televisor LED 55"', 'UN55AU7700', 'Samsung', 2023, '110V', '1.5A', 'Tela LCD 55 polegadas', 'Cartão de Crédito', '7 dias úteis', 'Reparo concluído com sucesso, tela substituída', 500.00, 450.00),
('Maria Oliveira Costa', '987.654.321-02', '98.765.432-1', '(11) 99876-5432', 'Av. Principal, 456', 'Jardim das Rosas', 'São Paulo', '04567-890', 'Geladeira Frost Free', 'BRM54HK', 'Brastemp', 2022, '220V', '2.0A', 'Compressor Geladeira', 'PIX', '10 dias úteis', 'Compressor substituído, sistema testado e aprovado', 350.00, 320.00),
('Carlos Mendes Lima', '456.789.123-03', '45.678.912-3', '(11) 97654-3210', 'Rua do Comércio, 789', 'Vila Nova', 'São Paulo', '02345-678', 'Micro-ondas', 'NN-ST67H', 'Panasonic', 2023, '110V', '12A', 'Magnetron Micro-ondas', 'Dinheiro', '5 dias úteis', 'Magnetron substituído, aquecimento normalizado', 180.00, 150.00),
('Ana Paula Ferreira', '321.654.987-04', '32.165.498-7', '(11) 96543-2109', 'Rua da Paz, 321', 'Bela Vista', 'São Paulo', '03456-789', 'Ar Condicionado Split', 'S4-Q12JA3WF', 'LG', 2021, '220V', '5.5A', 'Evaporadora Split 12000 BTU', 'Cartão de Débito', '15 dias úteis', 'Evaporadora trocada, sistema de refrigeração funcionando', 750.00, 700.00);

-- =============================================================================
-- DADOS DE EXEMPLO - RELACIONAMENTOS N:N
-- =============================================================================

-- OrcamentoPecas (Orçamento × Peça)
INSERT INTO OrcamentoPecas (OrcamentoId, PecaId, Quantidade, PrecoUnitario) VALUES
(1, 1, 1, 450.00),  -- João - Tela LCD
(2, 2, 1, 280.00),  -- Maria - Compressor
(3, 3, 1, 120.00),  -- Carlos - Magnetron
(4, 4, 1, 650.00),  -- Ana - Evaporadora
(5, 5, 1, 180.00),  -- Roberto - Bateria
(1, 3, 2, 120.00),  -- João - Magnetron (orçamento extra)
(2, 6, 1, 320.00);  -- Maria - Tela iPhone (orçamento extra)

-- ReparoEquipamentos (Reparo × Equipamento)
INSERT INTO ReparoEquipamentos (ReparoId, EquipamentoId) VALUES
(1, 1),  -- Reparo 1 - TV Samsung
(2, 2),  -- Reparo 2 - Geladeira Brastemp
(3, 3),  -- Reparo 3 - Micro-ondas Panasonic
(4, 4);  -- Reparo 4 - Ar Condicionado LG

-- FornecedorPecas (Fornecedor × Peça)
INSERT INTO FornecedorPecas (FornecedorId, PecaId, PrecoUnitario, DataUltimaCompra) VALUES
(1, 1, 400.00, '2024-01-15'),  -- Tech Parts - Tela LCD
(1, 5, 160.00, '2024-02-10'),  -- Tech Parts - Bateria
(2, 2, 250.00, '2024-02-20'),  -- Eletrônicos SP - Compressor
(2, 4, 600.00, '2024-03-05'),  -- Eletrônicos SP - Evaporadora
(3, 3, 100.00, '2024-03-10'),  -- Componentes - Magnetron
(3, 7, 500.00, '2024-04-01'),  -- Componentes - Placa Mãe
(4, 6, 280.00, '2024-04-05'),  -- Peças Express - Tela iPhone
(4, 8, 250.00, '2024-04-12'),  -- Peças Express - Fonte ATX
(5, 1, 420.00, '2024-04-20'),  -- GlobalTech - Tela LCD
(5, 5, 170.00, '2024-04-25');  -- GlobalTech - Bateria