using AutoMapper;
using System.Collections.Generic;

namespace ReWork.Common
{
    public static class Mapping<Ts,Td>  where Ts : new() where Td : new()
    { 
        public static Td MapObject(Ts source) 
        {
            IMapper mapper = Config();
            return mapper.Map<Ts, Td>(source);
        }

        public static IEnumerable<Td> MapCollection(IEnumerable<Ts> source)
        {
            IMapper mapper = Config();
            return mapper.Map<IEnumerable<Ts>,IEnumerable<Td>>(source);
        }

        private static IMapper Config()
        {
            MapperConfiguration config = new MapperConfiguration(cfg => cfg.CreateMap<Ts, Td>());
            return config.CreateMapper();
        }
    }
}
