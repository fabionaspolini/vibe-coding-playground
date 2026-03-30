using Geografia.Application.Dtos;
using Geografia.Application.Interfaces;
using Geografia.Application.Mappings;
using Geografia.Domain.Entities;
using Geografia.Domain.Repositories;
using System.Linq.Expressions;

namespace Geografia.Application.Services;

public class PaisService(IPaisRepository repository, IKafkaProducer producer)
{
    private const string Topic = "geografia.pais";

    public async Task<PaisDto?> GetById(string id) => (await repository.GetByIdAsync(id))?.ToDto();

    public async Task<IEnumerable<PaisDto>> List(Expression<Func<Pais, bool>>? filter = null) => (await repository.ListAsync(filter)).Select(p => p.ToDto());

    public async Task<PaisDto> Create(PaisRequest request)
    {
        var entity = request.ToEntity();
        await repository.AddAsync(entity);
        await repository.SaveChangesAsync();
        producer.Produce(Topic, entity.Id, entity.ToDto());
        return entity.ToDto();
    }

    public async Task<PaisDto?> Update(string id, PaisRequest request)
    {
        var entity = await repository.GetByIdAsync(id);
        if (entity == null) return null;
        entity.UpdateFromRequest(request);
        await repository.UpdateAsync(entity);
        await repository.SaveChangesAsync();
        producer.Produce(Topic, entity.Id, entity.ToDto());
        return entity.ToDto();
    }

    public async Task<bool> Remove(string id)
    {
        var entity = await repository.GetByIdAsync(id);
        if (entity == null) return false;
        entity.Ativo = false;
        await repository.UpdateAsync(entity);
        await repository.SaveChangesAsync();
        producer.Produce(Topic, entity.Id, entity.ToDto());
        return true;
    }
}

public class EstadoService(IEstadoRepository repository, IKafkaProducer producer)
{
    private const string Topic = "geografia.estado";

    public async Task<EstadoDto?> GetById(string id) => (await repository.GetByIdAsync(id))?.ToDto();

    public async Task<IEnumerable<EstadoDto>> List(Expression<Func<Estado, bool>>? filter = null) => (await repository.ListAsync(filter)).Select(e => e.ToDto());

    public async Task<EstadoDto> Create(EstadoRequest request)
    {
        var entity = request.ToEntity();
        await repository.AddAsync(entity);
        await repository.SaveChangesAsync();
        producer.Produce(Topic, entity.Id, entity.ToDto());
        return entity.ToDto();
    }

    public async Task<EstadoDto?> Update(string id, EstadoRequest request)
    {
        var entity = await repository.GetByIdAsync(id);
        if (entity == null) return null;
        entity.UpdateFromRequest(request);
        await repository.UpdateAsync(entity);
        await repository.SaveChangesAsync();
        producer.Produce(Topic, entity.Id, entity.ToDto());
        return entity.ToDto();
    }

    public async Task<bool> Remove(string id)
    {
        var entity = await repository.GetByIdAsync(id);
        if (entity == null) return false;
        entity.Ativo = false;
        await repository.UpdateAsync(entity);
        await repository.SaveChangesAsync();
        producer.Produce(Topic, entity.Id, entity.ToDto());
        return true;
    }
}

public class CidadeService(ICidadeRepository repository, IKafkaProducer producer)
{
    private const string Topic = "geografia.cidade";

    public async Task<CidadeDto?> GetById(Guid id) => (await repository.GetByIdAsync(id))?.ToDto();

    public async Task<IEnumerable<CidadeDto>> List(Expression<Func<Cidade, bool>>? filter = null) => (await repository.ListAsync(filter)).Select(c => c.ToDto());

    public async Task<CidadeDto> Create(CidadeRequest request)
    {
        var entity = request.ToEntity();
        await repository.AddAsync(entity);
        await repository.SaveChangesAsync();
        producer.Produce(Topic, entity.Id.ToString(), entity.ToDto());
        return entity.ToDto();
    }

    public async Task<CidadeDto?> Update(Guid id, CidadeRequest request)
    {
        var entity = await repository.GetByIdAsync(id);
        if (entity == null) return null;
        entity.UpdateFromRequest(request);
        await repository.UpdateAsync(entity);
        await repository.SaveChangesAsync();
        producer.Produce(Topic, entity.Id.ToString(), entity.ToDto());
        return entity.ToDto();
    }

    public async Task<bool> Remove(Guid id)
    {
        var entity = await repository.GetByIdAsync(id);
        if (entity == null) return false;
        entity.Ativo = false;
        await repository.UpdateAsync(entity);
        await repository.SaveChangesAsync();
        producer.Produce(Topic, entity.Id.ToString(), entity.ToDto());
        return true;
    }
}
