# -*- coding: utf-8 -*-


from zad2 import Aggregator


class TestAggregator(object):
    def setUp(self):
        self.aggregator = Aggregator.__new__(Aggregator)

    def func(self):
        pass

    def test_subscribe(self):
        self.aggregator.subscribe("test/name", self.func)

        assert "test/name" in self.aggregator._listeners
        assert self.func in self.aggregator._listeners["test/name"]

    def test_unsubscribe1(self):
        self.aggregator._listeners["test/name"] = [self.func]

        self.aggregator.unsubscribe("test/name", self.func)

        assert "test/name" not in self.aggregator._listeners

    def test_unsubscribe2(self):
        def func2():
            pass
        self.aggregator._listeners["test/name"] = [self.func, func2]

        self.aggregator.unsubscribe("test/name", self.func)

        assert "test/name" in self.aggregator._listeners
        assert self.aggregator._listeners["test/name"] == [func2]

    def test_publish1(self):
        def func3():
            func3.a = True
        func3.a = False
        self.aggregator._listeners["test/name"] = [func3]

        self.aggregator.publish("test/name")

        assert func3.a is True
