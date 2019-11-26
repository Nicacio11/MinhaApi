
    if exists (select * from dbo.sysobjects where id = object_id(N'Aluno') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table Aluno

    create table Aluno (
        aln_int_id INT IDENTITY NOT NULL,
       aln_str_nome NVARCHAR(255) null,
       aln_str_mensalidade DECIMAL(19,5) null,
       aln_str_endereco NVARCHAR(255) null,
       primary key (aln_int_id)
    )
