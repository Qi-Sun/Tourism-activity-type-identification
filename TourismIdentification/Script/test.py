import datetime
from Script.SinaWeibo import SinaWeibo
from Script.DatabaseHelper import get_one_weibo
from Script.TxtHelper import read_points_txt, read_city_distance_csv

if __name__ == '__main__':
    record = get_one_weibo()
    weibo = SinaWeibo.construct_from_record(record)
    city_distance = read_city_distance_csv('../Data/city_distance.csv')
    pois_attraction = read_points_txt('../Data/points_attraction.txt')
    pois_eat = read_points_txt('../Data/points_eat.txt')
    pois_accommodation = read_points_txt('../Data/points_accommodation.txt')
    pois_transportation = read_points_txt('../Data/points_transportation.txt')
    pois_buy = read_points_txt('../Data/points_buy.txt')
    pois_entertainment = read_points_txt('../Data/points_entertainment.txt')
    spatial_wide_vector, spatial_deep_vector = weibo.get_spatial_feature(city_distance, pois_attraction, pois_eat,
                                                                         pois_accommodation, pois_transportation,
                                                                         pois_buy, pois_entertainment)
    time_wide_vector,time_deep_vector = weibo.get_time_feature()
    print(spatial_wide_vector, spatial_deep_vector,time_wide_vector,time_deep_vector)

