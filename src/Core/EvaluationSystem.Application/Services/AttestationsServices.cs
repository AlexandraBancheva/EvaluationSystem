using AutoMapper;
using EvaluationSystem.Application.Interfaces;
using EvaluationSystem.Application.Models.Attestations;
using EvaluationSystem.Application.Models.Forms;
using EvaluationSystem.Application.Models.Participants;
using EvaluationSystem.Application.Models.Users;
using EvaluationSystem.Application.Repositories;
using EvaluationSystem.Domain.Entities;
using EvaluationSystem.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EvaluationSystem.Application.Services
{
    public class AttestationsServices : IAttestationsServices
    {
        private readonly IUserRepository _userRepository;
        private readonly IFormsServices _formsServices;
        private readonly IAttestationRepository _attestationRepository;
        private readonly IAttestationFormRepository _attestationFormRepository;
        private readonly IFormRepository _formRepository;
        private readonly IAttestationFormsServices _attestationFormsServices;
        private IMapper _mapper;

        public AttestationsServices(IUserRepository userRepository, 
                                    IAttestationRepository attestationRepository,
                                    IAttestationFormRepository attestationFormRepository,
                                    IFormsServices formsServices,
                                    IFormRepository formRepository,
                                    IAttestationFormsServices attestationFormsServices, 
                                    IMapper mapper)
        {
            _userRepository = userRepository;
            _attestationRepository = attestationRepository;
            _attestationFormRepository = attestationFormRepository;
            _formRepository = formRepository;
            _formsServices = formsServices;
            _attestationFormsServices = attestationFormsServices;
            _mapper = mapper;
        }

        public AttestationDetailDto CreateAttestation(CreateAttestationDto model)
        {
            var formId = model.IdForm;
            var isExist = _formRepository.GetById(formId);

            if (isExist == null)
            {
                throw new Exception("This form id doesn't exist!");
            }

            var form = _formRepository.GetAllByFormId(formId);
            var attestation = new AttestationDetailDto();

            foreach (var currForm in form)
            {
                var formForAttestation = _mapper.Map<CreateFormDto>(currForm);
                var attestationFormId = _attestationFormsServices.CreateNewForm(formForAttestation);

                var userForEvaluate = _userRepository.GetUserByEmail(model.User.Email);

                if (userForEvaluate == null)
                {
                    userForEvaluate = new User
                    {
                        Name = model.User.Name,
                        Email = model.User.Email,
                    };

                    var id = _userRepository.Insert(userForEvaluate);
                    userForEvaluate.Id = id;
                }

                if (model.UserParticipants.Count == 0)
                {
                    throw new Exception("Participants count cannot be zero!");
                }

                var participants = new List<UserParticipantDto>();

                foreach (var participant in model.UserParticipants)
                {
                    if (!participants.Any(x => x.Email == participant.Email))
                    {
                        participants.Add(participant);
                    }
                }

                var userParticipants = _mapper.Map<List<CreateUserParticipantsDto>>(participants);
                foreach (var userParticipant in userParticipants)
                {
                    var current = _userRepository.GetUserByEmail(userParticipant.Email);

                    if (current == null)
                    {
                        current = new User
                        {
                            Name = userParticipant.Name,
                            Email = userParticipant.Email,
                        };
                        var id = _userRepository.Insert(current);
                        current.Id = id;
                    }
                    userParticipant.Id = current.Id;
                }

                var attestationForInsert = new Attestation
                {
                    IdForm = attestationFormId,
                    IdUserToEvaluate = userForEvaluate.Id,
                    CreateDate = DateTime.UtcNow,
                };

                var attestationId = _attestationRepository.Insert(attestationForInsert);
                foreach (var userParticipant in userParticipants)
                {
                    _attestationRepository.AddUserParticipantsToAttestation(attestationId, userParticipant.Id, userParticipant.Position);
                }

                var attestationForm = _attestationFormRepository.GetById(attestationFormId);

                attestation = new AttestationDetailDto
                {
                    Id = attestationId,
                    Username = userForEvaluate.Name,
                    FormName = attestationForm.Name,
                    Status = Status.Open.ToString(),
                    UserParticipants = new List<ParticipantDetailDto>(),
                };

                foreach (var participant in userParticipants)
                {
                    attestation.UserParticipants.Add(_mapper.Map<ParticipantDetailDto>(participant));
                }
            }
            return attestation;
        }

        public void DeleteAttestation(int attestationId)
        {
            var attestation = _attestationRepository.GetById(attestationId);
            // Delete from useranswersServices!
            _attestationRepository.DeleteAttestation(attestationId);

            _attestationFormRepository.DeleteAttestationForm(attestation.IdForm);
        }

        public ICollection<AttestationInfoDbDto> GetAllAttestations()
        {
            var attestations = _attestationRepository.GetAllAttestation();

            foreach (var attestation in attestations)
            {
                if (attestation.Participants.All(p => p.ParticipantStatus == Status.Done))
                {
                    attestation.Status = Status.Done;
                }
                else if (attestation.Participants.All(p => p.ParticipantStatus == Status.Open))
                {
                    attestation.Status = Status.Open;
                }
                else
                {
                    attestation.Status = Status.InProgress;
                }
            }

            return attestations;
        }
    }
}
