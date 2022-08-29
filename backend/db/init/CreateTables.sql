USE admin

CREATE TABLE IF NOT EXISTS Filme (
    Id INT NOT NULL auto_increment PRIMARY KEY,
    Titulo VARCHAR(100),
    ClassificacaoIndicativa INT,
    Lancamento TINYINT
);

CREATE TABLE IF NOT EXISTS Cliente (
    Id INT NOT NULL auto_increment PRIMARY KEY,
    Nome VARCHAR(200),
    CPF VARCHAR(11),
    DataNascimento DATETIME
);

CREATE TABLE IF NOT EXISTS Locacao (
    id INT NOT NULL auto_increment PRIMARY KEY,
    Id_Cliente INT,
    Id_Filme INT,
    DataLocacao DATETIME DEFAULT CURRENT_TIMESTAMP,
    DataDevolucao DATETIME ON UPDATE CURRENT_TIMESTAMP,
    CONSTRAINT FK_Cliente_idx FOREIGN KEY (Id_Cliente) REFERENCES Cliente(Id) ON DELETE CASCADE,
    CONSTRAINT FK_Filme_idx FOREIGN KEY (Id_Filme) REFERENCES Filme(Id) ON DELETE CASCADE
);

CREATE INDEX idx_Lancamento ON Filme (Lancamento);
CREATE INDEX idx_Titulo ON Filme (Titulo);
CREATE INDEX idx_CPF ON Cliente (CPF);
CREATE INDEX idx_NOME ON Cliente (Nome);

SET character_set_client = utf8;
SET character_set_connection = utf8;
SET character_set_results = utf8;
SET collation_connection = utf8_general_ci;
