using GestaoJogosEFCore.Models;
using GestaoJogosEFCore.Repositories;

namespace GestaoJogosEFCore.Services
{
    public class JogoServece
    {
        private readonly JogoRepository _repository;

        public JogoServece(JogoRepository repository)
        {
            _repository = repository;
        }
     
        public List<Jogo> ListarTodosJogos() => _repository.Listar();
        
        public Jogo? BuscarPorId(int id) => _repository.BuscarPorId(id);
        
        public void CadastrarJogo(Jogo jogo) => _repository.Adicionar(jogo);

        public bool AtualizarJogo(Jogo jogoAtualizado) => _repository.Atualizar(jogoAtualizado);

        public bool ExcluirJogo(int id) => _repository.Excluir(id);

        public List<Desenvolvedora> ListarDesenvolvedoras() => _repository.ListarDesenvolvedoras();

        public List<Desenvolvedora> CadastrarDesenvolvedora(Desenvolvedora desenvolvedora) => _repository.CadastrarDesenvolvedora(desenvolvedora);
    }
}