INSERT INTO Enderecos (CEP, NumeroCasa, Bairro, Cidade, UF, Logradouro) VALUES
('01001000', 100, 'Centro', 'São Paulo', 'SP', 'Praça da Sé'),
('20040002', 200, 'Centro', 'Rio de Janeiro', 'RJ', 'Rua da Assembleia'),
('30140071', 50,  'Funcionários', 'Belo Horizonte', 'MG', 'Avenida Afonso Pena'),
('40020003', 75,  'Comércio', 'Salvador', 'BA', 'Rua Chile'),
('70040902', 15,  'Zona Cívico-Administrativa', 'Brasília', 'DF', 'Praça dos Três Poderes'),
('80020020', 320, 'Centro', 'Curitiba', 'PR', 'Rua XV de Novembro'),
('50020040', 180, 'Santo Amaro', 'Recife', 'PE', 'Avenida Guararapes'),
('66020030', 55,  'Cidade Velha', 'Belém', 'PA', 'Travessa Padre Eutíquio'),
('90020030', 88,  'Centro Histórico', 'Porto Alegre', 'RS', 'Rua da Praia'),
('69005080', 42,  'Centro', 'Manaus', 'AM', 'Avenida Eduardo Ribeiro'),
('57020000', 110, 'Ponta Verde', 'Maceió', 'AL', 'Avenida Dr. Antônio Gouveia'),
('78020050', 67,  'Centro', 'Cuiabá', 'MT', 'Rua Comandante Costa');

INSERT INTO Tutores (Nome, DataNascimento, EnderecoId) VALUES
('Ana Beatriz Silva',       '1985-03-15', 1),
('Carlos Eduardo Santos',   '1990-07-22', 2),
('Mariana Oliveira Costa',  '1988-01-10', 3),
('Pedro Henrique Almeida',  '1975-11-05', 4),
('Juliana Pereira Lima',    '1995-06-18', 5),
('Rafael Mendes Souza',     '1982-09-30', 6),
('Fernanda Rocha Barbosa',  '1993-04-12', 7),
('Lucas Martins Teixeira',  '1980-12-08', 8),
('Amanda Carvalho Dias',    '1998-08-25', 9),
('Bruno Oliveira Ribeiro',  '1987-05-14', 10),
('Larissa Faria Neves',     '1991-02-20', 11);

INSERT INTO Animais (Nome, Especie, Peso, DataNascimento, TutorId) VALUES
('Rex',    'Cachorro', 25.50, '2020-03-10', 1),
('Mimi',   'Gato',     4.20, '2021-07-22', 2),
('Pipoca', 'Ave',      0.35, '2023-01-15', 3),
('Thor',   'Cachorro', 32.00, '2019-11-05', 4),
('Chico',  'Gato',     5.10, '2022-06-18', 5),
('Bolinha','Hamster',  0.15, '2024-02-28', 6),
('Mel',    'Cachorro', 18.70, '2021-09-30', 7),
('Frajola','Gato',     4.80, '2020-12-12', 8),
('Nemo',   'Peixe',    0.05, '2024-04-01', 9),
('Luna',   'Cachorro', 22.30, '2018-08-25', 10),
('Simba',  'Gato',     6.00, '2023-05-14', 11),
('Toddy',  'Cachorro', 28.90, '2020-10-10', 1),
('Nala',   'Gato',     3.80, '2022-03-20', 3),
('Pérola', 'Coelho',   2.50, '2023-07-12', 5),
('Bob',    'Tartaruga', 1.20, '2019-04-15', 7);
