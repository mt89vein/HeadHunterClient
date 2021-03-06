﻿namespace Domain.Enums
{
    /// <summary>
    /// Тип водительского удостоверения
    /// </summary>
    public enum DriverLicenseType
    {
        /// <summary>
        /// Мотоцикл с коляской или без общей массой до 400 кг
        /// </summary>
        A,

        /// <summary>
        /// Легковые автомобили общей массой до 3.5 тонн, с прицепом до 750кг
        /// </summary>
        B,

        /// <summary>
        /// Автомобили более 3.5 тонн, с прицепом до 750кг
        /// </summary>
        C,

        /// <summary>
        /// Автомобили для пассажирских перевозок, с количеством мест более 8
        /// </summary>
        D,

        /// <summary>
        /// Дополнение к категориям B,C,D
        /// </summary>
        E,

        /// <summary>
        /// Дополнение к категории B, снимает ограничение на массу прицепа
        /// </summary>
        BE,

        /// <summary>
        /// Дополнение к категории C, снимает ограничение на массу прицепа
        /// </summary>
        CE,

        /// <summary>
        /// Дополнение к категории D, снимает ограничение на массу прицепа
        /// </summary>
        DE,

        /// <summary>
        /// Трамвай
        /// </summary>
        TM,

        /// <summary>
        /// Троллейбус
        /// </summary>
        TB
    }
}