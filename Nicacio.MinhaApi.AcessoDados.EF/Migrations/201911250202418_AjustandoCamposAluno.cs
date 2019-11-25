namespace Nicacio.MinhaApi.AcessoDados.EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AjustandoCamposAluno : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Alunoes", newName: "Aluno");
            RenameColumn(table: "dbo.Aluno", name: "Id", newName: "aln_int_id");
            RenameColumn(table: "dbo.Aluno", name: "Nome", newName: "aln_str_nome");
            RenameColumn(table: "dbo.Aluno", name: "Endereco", newName: "aln_str_endereco");
            RenameColumn(table: "dbo.Aluno", name: "Mensalidade", newName: "aln_dec_mensalidade");
        }
        
        public override void Down()
        {
            RenameColumn(table: "dbo.Aluno", name: "aln_dec_mensalidade", newName: "Mensalidade");
            RenameColumn(table: "dbo.Aluno", name: "aln_str_endereco", newName: "Endereco");
            RenameColumn(table: "dbo.Aluno", name: "aln_str_nome", newName: "Nome");
            RenameColumn(table: "dbo.Aluno", name: "aln_int_id", newName: "Id");
            RenameTable(name: "dbo.Aluno", newName: "Alunoes");
        }
    }
}
