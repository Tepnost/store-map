﻿using StoreMap.Data.Dtos;

namespace StoreMap.Backend.Data.Entities
{
    public class StoreObjectCircle : StoreObject
    {
        public int Diameter { get; set; }

        public static StoreObjectCircle FromDto(StoreObjectCircleDto dto)
        {
            return new StoreObjectCircle
            {
                X = dto.X,
                Y = dto.Y,
                Color = dto.Color,
                Diameter = dto.Diameter
            };
        }

        public override StoreObjectDto ToDto()
        {
            return new StoreObjectCircleDto
            {
                X = X,
                Y = Y,
                Color = Color,
                Diameter = Diameter
            };
        }
    }
}