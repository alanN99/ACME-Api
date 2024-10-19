using AutoMapper;
using ACME_Api.Models;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        // Define mapeos entre modelos y DTOs
        CreateMap<Student, StudentDTO>().ReverseMap();
        CreateMap<Course, CourseDTO>().ReverseMap();

        // Mapeo entre EnrollRequestDTO y Enrollment
        CreateMap<EnrollRequestDTO, Enrollment>()
            .ForMember(dest => dest.StudentId, opt => opt.MapFrom(src => src.StudentId))
            .ForMember(dest => dest.CourseId, opt => opt.MapFrom(src => src.CourseId))
            .ForMember(dest => dest.PaymentAmount, opt => opt.MapFrom(src => src.PaymentAmount))
            .ForMember(dest => dest.PaymentToken, opt => opt.MapFrom(src => src.PaymentToken));
    }
}