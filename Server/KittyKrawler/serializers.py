from rest_framework import serializers
from KittyKrawler.models import GameSave, Leaderboard, Item


class ItemListField(serializers.RelatedField):
    def to_representation(self, value):
        return value.item_id

    def to_internal_value(self, data):
        return data

    def get_queryset(self):
        return Item.objects.filter()


class SaveSerializer(serializers.ModelSerializer):
    user_name = serializers.ReadOnlyField(source='user.username')
    item_list = ItemListField(source='save_items', many=True)

    class Meta:
        model = GameSave
        fields = ('user_name', 'item_list', 'kill_counter', 'attack', 'defence', 'speed', 'health', 'total_health', 'next_level', 'time')

    def create(self, validated_data):
        item_list = validated_data.pop('save_items')
        game_save = GameSave.objects.create(**validated_data)
        for item_num in item_list:
            try:
                item = Item.objects.get(item_id=item_num)
                game_save.save_items.add(item)
            except Item.DoesNotExist:
                item = Item.objects.create(item_id=item_num)
                game_save.save_items.add(item)

        return game_save


class LeaderboardSerializer(serializers.ModelSerializer):
    class Meta:
        model = Leaderboard
        fields = ('user', 'game_save')


class ItemSerializer(serializers.ModelSerializer):
    class Meta:
        model = Item
        fields = ('item_id',)