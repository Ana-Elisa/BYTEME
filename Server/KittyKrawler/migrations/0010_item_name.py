# -*- coding: utf-8 -*-
# Generated by Django 1.10.5 on 2017-04-15 20:26
from __future__ import unicode_literals

from django.db import migrations, models


class Migration(migrations.Migration):

    dependencies = [
        ('KittyKrawler', '0009_auto_20170324_2004'),
    ]

    operations = [
        migrations.AddField(
            model_name='item',
            name='name',
            field=models.CharField(default='', max_length=25),
            preserve_default=False,
        ),
    ]
