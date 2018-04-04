namespace Compline.Teste.Api.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModelagemDB : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ListaDeTarefas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NomeLista = c.String(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                        UpdatedAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Tarefas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Descricao = c.String(nullable: false),
                        PrioridadeId = c.Int(nullable: false),
                        ListaDeTarefasId = c.Int(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                        UpdatedAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Prioridades", t => t.PrioridadeId, cascadeDelete: true)
                .ForeignKey("dbo.ListaDeTarefas", t => t.ListaDeTarefasId, cascadeDelete: true)
                .Index(t => t.PrioridadeId)
                .Index(t => t.ListaDeTarefasId);
            
            CreateTable(
                "dbo.Prioridades",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Descricao = c.String(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                        UpdatedAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tarefas", "ListaDeTarefasId", "dbo.ListaDeTarefas");
            DropForeignKey("dbo.Tarefas", "PrioridadeId", "dbo.Prioridades");
            DropIndex("dbo.Tarefas", new[] { "ListaDeTarefasId" });
            DropIndex("dbo.Tarefas", new[] { "PrioridadeId" });
            DropTable("dbo.Prioridades");
            DropTable("dbo.Tarefas");
            DropTable("dbo.ListaDeTarefas");
        }
    }
}
