using AutoMapper;
using Blogy.Business.DTOs.CommentDtos;
using Blogy.DataAccess.Repositories.CommentRepositories;
using Blogy.Entity.Entities;

namespace Blogy.Business.Services.CommentServices
{
    public class CommentService(ICommentRepository _commentRepository,
                                IMapper _mapper) : ICommentService
    {
        public async Task CreateAsync(CreateCommentDto createDto)
        {
            var comment = _mapper.Map<Comment>(createDto);
            if (createDto.IsToxic)
            {
                comment.CommentStatus = false; 
                comment.IsToxic = true;       
            }
            else
            {
                comment.CommentStatus = true;
                comment.IsToxic = false;
            }
            comment.CreatedDate = DateTime.Now;
            await _commentRepository.CreateAsync(comment);
        }

        public async Task DeleteAsync(int id)
        {
            await _commentRepository.DeleteAsync(id);
        }

        public async Task<List<ResultCommentDto>> GetAllAsync()
        {
            var values = await _commentRepository.GetAllAsync();
            return _mapper.Map<List<ResultCommentDto>>(values);
        }

        public async Task<UpdateCommentDto> GetByIdAsync(int id)
        {
            var value = await _commentRepository.GetByIdAsync(id);
            return _mapper.Map<UpdateCommentDto>(value);
        }

        public async Task<ResultCommentDto> GetSingleByIdAsync(int id)
        {
            var value = await _commentRepository.GetByIdAsync(id);
            return _mapper.Map<ResultCommentDto>(value);
        }

        public async Task UpdateAsync(UpdateCommentDto updateDto)
        {
            var comment = _mapper.Map<Comment>(updateDto);
           

            await _commentRepository.UpdateAsync(comment);
        }
    }
}
