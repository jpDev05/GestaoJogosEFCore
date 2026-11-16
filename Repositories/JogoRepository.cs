using GestaoJogosEFCore.Data;
using GestaoJogosEFCore.Models;
using Microsoft.EntityFrameworkCore;

namespace GestaoJogosEFCore.Repositories
{

    public class JogoRepository
    {
        private readonly DbContextOptions<GestaoJogosContext> _options;

        public JogoRepository(DbContextOptions<GestaoJogosContext> options)
        {
            _options = options;
        }

        public List<Jogo> Listar()
        {
            using var context = new GestaoJogosContext(_options);

            return context.Jogo
            .Include(j => j.IdDesenvolvedoraNavigation)
            .OrderBy(j => j.IdJogo)
            .ToList();
            //.Include(j => j.IdDesenvolvedoraNavigation) // Inclui a entidade relacionada Desenvolvedora
            //.Include(j => j.IdGeneroNavigation) // Inclui a entidade relacionada Genero
            //.Include(j => j.IdPlataformaNavigation) // Inclui a entidade relacionada Plataforma
            //.Include(j => j.IdDistribuidoraNavigation) // Inclui a entidade relacionada Distribuidora
            //.Include(j => j.IdClassificacaoNavigation) // Inclui a entidade relacionada Classificacao
            //.Include(j => j.IdIdiomaNavigation) // Inclui a entidade relacionada Idioma
            //.Include(j => j.IdLojaNavigation) // Inclui a entidade relacionada Loja
            
        }

        public Jogo? BuscarPorId(int id)
        {
            using var context = new GestaoJogosContext(_options);

            return context.Jogo
                .Include(j => j.IdDesenvolvedoraNavigation)
                .FirstOrDefault(j => j.IdJogo == id);
        }

        public void Adicionar(Jogo jogo)
        {
            using var context = new GestaoJogosContext(_options);

            context.Jogo.Add(jogo);
            context.SaveChanges(); // Salva as alterações no banco de dados
        }

        public bool Atualizar(Jogo jogoAtualizado)
        {
            using var context = new GestaoJogosContext(_options);

            var jogoExistente = context.Jogo.Find(jogoAtualizado.IdJogo);
            if (jogoExistente is null)
            {
                return false;
            }

            jogoExistente.IdDesenvolvedora = jogoAtualizado.IdDesenvolvedora;
            jogoExistente.Nome = jogoAtualizado.Nome;
            jogoExistente.AnoLancamento = jogoAtualizado.AnoLancamento;  
            jogoExistente.Genero = jogoAtualizado.Genero;
            jogoExistente.Preco = jogoAtualizado.Preco;
            context.SaveChanges();
            return true; // Salva as alterações no banco de dados
        }

      public bool Excluir(int id)
{
    using var context = new GestaoJogosContext(_options);

    var jogoExistente = context.Jogo.Find(id);
    if (jogoExistente is null)
    {
        return false;
    }

    // Remove primeiro os vínculos na tabela JogadorJogo
    var vinculos = context.JogadorJogo.Where(x => x.IdJogo == id).ToList();
    context.JogadorJogo.RemoveRange(vinculos);

    context.Jogo.Remove(jogoExistente);
    context.SaveChanges();
    return true;
}

        public List<Desenvolvedora> ListarDesenvolvedoras()
        {
            using var context = new GestaoJogosContext(_options);
            return context.Desenvolvedora.ToList();
        }

        public List<Desenvolvedora> CadastrarDesenvolvedora(Desenvolvedora desenvolvedora)
        {
            using var context = new GestaoJogosContext(_options);
            context.Desenvolvedora.Add(desenvolvedora);
            context.SaveChanges();
            return context.Desenvolvedora.ToList();
        }

    }
}