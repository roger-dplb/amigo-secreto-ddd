using AmigoSecreto.Domain.Entities;
using AmigoSecreto.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace AmigoSecreto.Infrastructure.Data
{
    /// <summary>
    /// Classe responsável por popular o banco de dados com dados iniciais para testes.
    /// </summary>
    public static class DatabaseSeeder
    {
        public static void Seed(ApplicationDbContext context)
        {
            // Garante que o banco foi criado
            context.Database.EnsureCreated();

            // Se já tem dados, não faz nada
            if (context.Users.Any())
            {
                Console.WriteLine("Banco já possui dados. Seed ignorado.");
                return;
            }

            Console.WriteLine("Iniciando seed do banco de dados...");

            // ===================================
            // 1️⃣ CRIAR USUÁRIOS
            // ===================================
            var user1 = new User("João Silva", "joao@email.com", "hash_senha_joao_123");
            var user2 = new User("Maria Santos", "maria@email.com", "hash_senha_maria_456");
            var user3 = new User("Pedro Oliveira", "pedro@email.com", "hash_senha_pedro_789");
            var user4 = new User("Ana Costa", "ana@email.com", "hash_senha_ana_321");
            var user5 = new User("Carlos Souza", "carlos@email.com", "hash_senha_carlos_654");

            context.Users.AddRange(user1, user2, user3, user4, user5);
            context.SaveChanges(); // Salva para gerar os IDs
            Console.WriteLine("✅ 5 usuários criados");

            // ===================================
            // 2️⃣ CRIAR SUGESTÕES DE PRESENTES
            // ===================================
            var gift1 = new Gift("Livro Clean Architecture", 50.00m, user1.Id, "Livro do Uncle Bob sobre arquitetura limpa");
            var gift2 = new Gift("Mouse Gamer", 150.00m, user2.Id, "Mouse RGB com 16000 DPI");
            var gift3 = new Gift("Fone Bluetooth", 200.00m, user3.Id, "Fone com cancelamento de ruído");
            var gift4 = new Gift("Teclado Mecânico", 300.00m, user4.Id, "Teclado mecânico RGB switches blue");
            var gift5 = new Gift("Webcam Full HD", 250.00m, user5.Id, "Webcam 1080p 60fps");

            context.Gifts.AddRange(gift1, gift2, gift3, gift4, gift5);
            context.SaveChanges();
            Console.WriteLine("✅ 5 sugestões de presentes criadas");

            // ===================================
            // 3️⃣ CRIAR GRUPOS
            // ===================================
            var group1 = new Group(
                name: "Amigo Secreto da Família Silva",
                description: "Amigo secreto de Natal 2026 da família",
                minValue: 50.00m,
                maxValue: 200.00m,
                ownerId: user1.Id,
                happenAt: new DateTime(2026, 12, 25)
            );

            var group2 = new Group(
                name: "Amigo Secreto do Trabalho",
                description: "Confraternização de fim de ano da empresa",
                minValue: 100.00m,
                maxValue: 300.00m,
                ownerId: user2.Id,
                happenAt: new DateTime(2026, 12, 20)
            );

            context.Groups.Add(group1);
            context.Groups.Add(group2);
            context.SaveChanges();
            Console.WriteLine("✅ 2 grupos criados");

            // ===================================
            // 4️⃣ ADICIONAR MODERADORES E PARTICIPANTES
            // ===================================
            // Grupo 1: João é owner, Maria é moderadora
            // Participantes: João, Maria, Pedro, Ana
            context.Entry(group1).Collection(g => g.Moderators).Load();
            context.Entry(group1).Collection(g => g.Participants).Load();

            group1.Moderators.Add(user2); // Maria é moderadora
            group1.Participants.Add(user1); // João participa
            group1.Participants.Add(user2); // Maria participa
            group1.Participants.Add(user3); // Pedro participa
            group1.Participants.Add(user4); // Ana participa

            // Grupo 2: Maria é owner, Carlos é moderador
            // Participantes: Maria, Carlos, Pedro
            context.Entry(group2).Collection(g => g.Moderators).Load();
            context.Entry(group2).Collection(g => g.Participants).Load();

            group2.Moderators.Add(user5); // Carlos é moderador
            group2.Participants.Add(user2); // Maria participa
            group2.Participants.Add(user5); // Carlos participa
            group2.Participants.Add(user3); // Pedro participa

            context.SaveChanges();
            Console.WriteLine("✅ Moderadores e participantes adicionados aos grupos");

            // ===================================
            // 5️⃣ CRIAR SORTEIOS
            // ===================================
            var draw1 = new Draw(group1.Id);
            context.Draws.Add(draw1);
            context.SaveChanges();
            Console.WriteLine("✅ 1 sorteio criado para o Grupo 1");

            // ===================================
            // 6️⃣ CRIAR MATCHES DO SORTEIO (quem tirou quem)
            // ===================================
            // Grupo 1: João → Maria, Maria → Pedro, Pedro → Ana, Ana → João
            var match1 = new DrawMatch(draw1.Id, user1.Id, user2.Id); // João tirou Maria
            var match2 = new DrawMatch(draw1.Id, user2.Id, user3.Id); // Maria tirou Pedro
            var match3 = new DrawMatch(draw1.Id, user3.Id, user4.Id); // Pedro tirou Ana
            var match4 = new DrawMatch(draw1.Id, user4.Id, user1.Id); // Ana tirou João

            context.DrawMatches.AddRange(match1, match2, match3, match4);
            context.SaveChanges();
            Console.WriteLine("✅ 4 matches criados para o sorteio");

            // ===================================
            // 7️⃣ COMPLETAR O SORTEIO
            // ===================================
            draw1.Complete();
            context.SaveChanges();
            Console.WriteLine("✅ Sorteio marcado como completado");

            // ===================================
            // 8️⃣ REVELAR ALGUNS MATCHES
            // ===================================
            match1.MarkAsRevealed(); // João já viu que tirou Maria
            match2.MarkAsRevealed(); // Maria já viu que tirou Pedro
            context.SaveChanges();
            Console.WriteLine("✅ 2 matches revelados");

            Console.WriteLine("\n🎉 Seed concluído com sucesso!");
            Console.WriteLine("\n📊 Resumo dos dados criados:");
            Console.WriteLine($"   - {context.Users.Count()} usuários");
            Console.WriteLine($"   - {context.Gifts.Count()} sugestões de presentes");
            Console.WriteLine($"   - {context.Groups.Count()} grupos");
            Console.WriteLine($"   - {context.Draws.Count()} sorteios");
            Console.WriteLine($"   - {context.DrawMatches.Count()} matches");
        }
    }
}
