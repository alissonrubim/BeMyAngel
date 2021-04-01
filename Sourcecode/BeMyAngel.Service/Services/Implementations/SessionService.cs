using AutoMapper;
using BeMyAngel.Persistance.Models;
using BeMyAngel.Persistance.Repositories;
using BeMyAngel.Service.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BeMyAngel.Service.Services.Implementations
{
    internal class SessionService : ISessionService
    {
        private readonly ISessionRepository _repository;
        private readonly IMapper _mapper;
        public SessionService(ISessionRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public void AttachUser(Session session, User user)
        {
            if (!GetById(session.SessionId).UserId.HasValue)
            {
                session.UserId = user.UserId;
                _repository.Update(_mapper.Map<SessionDto>(session));
            }
        }

        public void DeattachUser(Session session)
        {
            if (GetById(session.SessionId).UserId.HasValue)
            {
                session.UserId = null;
                _repository.Update(_mapper.Map<SessionDto>(session));
            }
        }

        public int Create(Session session)
        {
            do
            {
                session.Token = Guid.NewGuid().ToString().ToUpper();
            }
            while (GetByToken(session.Token) != null);

            session.CreatedAt = DateTime.Now;
            session.LastAccessAt = DateTime.Now;
            return _repository.Insert(_mapper.Map<SessionDto>(session));
        }

        public Session GetById(int SessionId)
        {
            return _mapper.Map<Session>(_repository.GetById(SessionId));
        }

        public Session GetByToken(string Token)
        {
            return _mapper.Map<Session>(_repository.GetByToken(Token));
        }

        public void Renew(Session session)
        {
            session.LastAccessAt = DateTime.Now;
            _repository.Update(_mapper.Map<SessionDto>(session));
        }
    }
}
